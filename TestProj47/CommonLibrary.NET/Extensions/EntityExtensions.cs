﻿#if NetFX
using System;
using HSNXT.ComLib.Authentication;
using HSNXT.ComLib.Entities;
using HSNXT.ComLib.Feeds;

namespace HSNXT
{
    public static partial class Extensions
    {
        /// <summary>
        /// Whether or not the user supplied is the owner.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static bool IsOwner(this IEntity entity)
        {
            if (entity == null)
                return false;
            return Auth.IsUser(entity.CreateUser);
        }


        /// <summary>
        /// Whether or not the user supplied is the owner.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool IsOwner(this IEntity entity, string user)
        {
            if (entity == null)
                return false;
            return string.Compare(entity.CreateUser, user, true) == 0;
        }


        /// <summary>
        /// Whether or not the current user is the entity owner or admin.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static bool IsOwnerOrAdmin(this IEntity entity)
        {
            if (entity == null)
                return false;

            return Auth.IsUserOrAdmin(entity.CreateUser);
        }


        /// <summary>
        /// Whether or not the current user is the entity owner or admin.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        public static bool IsOwnerOrAdminOrInRoles(this IEntity entity, string roles)
        {
            if (entity == null)
                return false;

            var isMatch = Auth.IsUserOrAdmin(entity.CreateUser);
            if (!isMatch)
                isMatch = Auth.IsUserInRoles(roles);

            return isMatch;
        }


        /// <summary>
        /// Whether or not the current user is the entity owner or admin.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static bool IsOkToServe(this IEntity entity)
        {
            // Check for deleted items. This could happen if user deletes, hits back button in browser and selects the entity.
            if (entity == null) return false;

            // Now check access rights
            if (entity.IsOwnerOrAdmin()) return true;

            if (entity is IPublishable)
            {
                var publishable = entity as IPublishable;
                if (!publishable.IsPublished || !publishable.IsPublic)
                    return false;
            }
            return true;
        }


        /// <summary>
        /// Whether or the current user can copy this post.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static bool IsCopyable(this IEntity entity)
        {
            return IsOwnerOrAdmin(entity);
        }


        /// <summary>
        /// Convert this entity to a new copy by resetting the id to indicate it's not persistant.
        /// </summary>
        /// <param name="entity"></param>
        public static void ToNewCopy(this IEntity entity)
        {
            entity.Id = 0;
        }


        /// <summary>
        /// Sets up all the audit fields.
        /// </summary>
        /// <param name="entity"></param>
        public static void AuditAll(this IEntity entity)
        {
            if (entity == null) return;

            var username = Auth.UserShortName;
            var date = DateTime.Now;
            var timeStamp = date.ToShortDateString() + " - " + date.ToLongTimeString();

            if (string.IsNullOrEmpty(entity.CreateUser))
                entity.CreateUser = username;

            if (entity.CreateDate == DateTime.MinValue)
                entity.CreateDate = date;

            entity.UpdateUser = username;
            entity.UpdateDate = date;            
            entity.UpdateComment = "Updated " + timeStamp + " by " + username;
        }


        /// <summary>
        /// Update the model but keeps the privaty/audit fields from being updated.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="update"></param>
        public static void DoUpdateModel<T>(this IEntity entity, Action<T> update)
        {
            if (entity == null) return;

            // 1. Get the original values.            
            var id = entity.Id;
            var createDate = entity.CreateDate;
            var updateDate = entity.UpdateDate;
            var createUser = entity.CreateUser;
            var updateUser = entity.UpdateUser;
            var comment = entity.UpdateComment;
            var version = 0;
            var versionRefId = 0;
            var hasVersion = entity is IEntityVersioned;            
            IEntityVersioned versioned = null;
            if (hasVersion)
            {
                versioned = entity as IEntityVersioned;
                version = versioned.Version;
                versionRefId = versioned.VersionRefId;
            }

            // 2. Update model from the form values.
            update((T)entity);

            // 3. Now apply the values back 
            entity.Id = id;
            entity.CreateUser = createUser;
            entity.UpdateUser = updateUser;
            entity.CreateDate = createDate;
            entity.UpdateDate = updateDate;
            entity.UpdateComment = comment;
            if (hasVersion)
            {
                versioned.Version = version;
                versioned.VersionRefId = versionRefId;
            }
        }


        /// <summary>
        /// Update the model but keeps the privaty/audit fields from being updated.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="original"></param>
        public static void DoUpdateModel<T>(this IEntity entity, IEntity original)
        {
            if (entity == null || original == null) return;
            entity.CreateUser = original.CreateUser;
            entity.CreateDate = original.CreateDate;
            entity.UpdateComment = entity.UpdateComment;
            entity.UpdateDate = entity.UpdateDate;
            entity.UpdateUser = entity.UpdateUser;

            if (entity is IEntityVersioned && original is IEntityVersioned)
            {
                var newVEntity = entity as IEntityVersioned;
                var origVEntity = original as IEntityVersioned;
                newVEntity.Version = origVEntity.Version;
                newVEntity.VersionRefId = origVEntity.VersionRefId;
            }
        }
    }
}
#endif