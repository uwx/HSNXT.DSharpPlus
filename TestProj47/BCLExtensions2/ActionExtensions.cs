using System;
using HSNXT.BCLExtensions2;

namespace HSNXT
{
    public partial class Extensions
    {
        /// <summary>
        /// Extension method to expose a <see cref="System.Action{T}"/> as an  <see cref="System.Action"/>.
        /// </summary>
        /// <typeparam name="TParameter">The type of the parameter.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        public static Action AsActionUsing<TParameter>(this Action<TParameter> action, TParameter parameter)
        {
            return () => action(parameter);
        }

        /// <summary>
        /// Extension method to expose a <see cref="System.Action{T1,T2}" /> as an  <see cref="System.Action" />.
        /// </summary>
        /// <typeparam name="T1">The type of parameter1.</typeparam>
        /// <typeparam name="T2">The type of parameter2.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="parameter2">The parameter2.</param>
        /// <returns></returns>
        public static Action AsActionUsing<T1, T2>(this Action<T1, T2> action, T1 parameter1, T2 parameter2)
        {
            return () => action(parameter1, parameter2);
        }

        /// <summary>
        /// Extension method to expose a <see cref="System.Action{T1,T2,T3}" /> as an  <see cref="System.Action" />.
        /// </summary>
        /// <typeparam name="T1">The type of parameter1.</typeparam>
        /// <typeparam name="T2">The type of parameter2.</typeparam>
        /// <typeparam name="T3">The type of parameter3.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="parameter2">The parameter2.</param>
        /// <param name="parameter3">The parameter3.</param>
        /// <returns></returns>
        public static Action AsActionUsing<T1, T2, T3>(this Action<T1, T2, T3> action, T1 parameter1, T2 parameter2, T3 parameter3)
        {
            return () => action(parameter1, parameter2, parameter3);
        }

        /// <summary>
        /// Extension method to expose a <see cref="System.Action{T1,T2,T3,T4}" /> as an  <see cref="System.Action" />.
        /// </summary>
        /// <typeparam name="T1">The type of the parameter1.</typeparam>
        /// <typeparam name="T2">The type of the parameter2.</typeparam>
        /// <typeparam name="T3">The type of the parameter3.</typeparam>
        /// <typeparam name="T4">The type of the parameter4.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="parameter2">The parameter2.</param>
        /// <param name="parameter3">The parameter3.</param>
        /// <param name="parameter4">The parameter4.</param>
        /// <returns></returns>
        public static Action AsActionUsing<T1, T2, T3,T4>(this Action<T1, T2, T3, T4> action, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4)
        {
            return () => action(parameter1, parameter2, parameter3, parameter4);
        }

        /// <summary>
        /// Extension method to expose a <see cref="System.Action{T1,T2,T3,T4,T5}" /> as an  <see cref="System.Action" />.
        /// </summary>
        /// <typeparam name="T1">The type of the parameter1.</typeparam>
        /// <typeparam name="T2">The type of the parameter2.</typeparam>
        /// <typeparam name="T3">The type of the parameter3.</typeparam>
        /// <typeparam name="T4">The type of the parameter4.</typeparam>
        /// <typeparam name="T5">The type of the parameter5.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="parameter2">The parameter2.</param>
        /// <param name="parameter3">The parameter3.</param>
        /// <param name="parameter4">The parameter4.</param>
        /// <param name="parameter5">The parameter5.</param>
        /// <returns></returns>
        public static Action AsActionUsing<T1, T2, T3, T4, T5>(this Action<T1, T2, T3, T4, T5> action, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5)
        {
            return () => action(parameter1, parameter2, parameter3, parameter4, parameter5);
        }

        /// <summary>
        /// Extension method to expose a <see cref="System.Action{T1,T2,T3,T4,T5,T6}" /> as an  <see cref="System.Action" />.
        /// </summary>
        /// <typeparam name="T1">The type of the parameter1.</typeparam>
        /// <typeparam name="T2">The type of the parameter2.</typeparam>
        /// <typeparam name="T3">The type of the parameter3.</typeparam>
        /// <typeparam name="T4">The type of the parameter4.</typeparam>
        /// <typeparam name="T5">The type of the parameter5.</typeparam>
        /// <typeparam name="T6">The type of the parameter6.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="parameter2">The parameter2.</param>
        /// <param name="parameter3">The parameter3.</param>
        /// <param name="parameter4">The parameter4.</param>
        /// <param name="parameter5">The parameter5.</param>
        /// <param name="parameter6">The parameter6.</param>
        /// <returns></returns>
        public static Action AsActionUsing<T1, T2, T3, T4, T5, T6>(this Action<T1, T2, T3, T4, T5, T6> action, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6)
        {
            return () => action(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6);
        }

        /// <summary>
        /// Extension method to expose a <see cref="System.Action{T1,T2,T3,T4,T5,T6,T7}" /> as an  <see cref="System.Action" />.
        /// </summary>
        /// <typeparam name="T1">The type of the parameter1.</typeparam>
        /// <typeparam name="T2">The type of the parameter2.</typeparam>
        /// <typeparam name="T3">The type of the parameter3.</typeparam>
        /// <typeparam name="T4">The type of the parameter4.</typeparam>
        /// <typeparam name="T5">The type of the parameter5.</typeparam>
        /// <typeparam name="T6">The type of the parameter6.</typeparam>
        /// <typeparam name="T7">The type of the parameter7.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="parameter2">The parameter2.</param>
        /// <param name="parameter3">The parameter3.</param>
        /// <param name="parameter4">The parameter4.</param>
        /// <param name="parameter5">The parameter5.</param>
        /// <param name="parameter6">The parameter6.</param>
        /// <param name="parameter7">The parameter7.</param>
        /// <returns></returns>
        public static Action AsActionUsing<T1, T2, T3, T4, T5, T6, T7>(this Action<T1, T2, T3, T4, T5, T6, T7> action, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7)
        {
            return () => action(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7);
        }

        /// <summary>
        /// Extension method to expose a <see cref="System.Action{T1,T2,T3,T4,T5,T6,T7,T8}" /> as an  <see cref="System.Action" />.
        /// </summary>
        /// <typeparam name="T1">The type of the parameter1.</typeparam>
        /// <typeparam name="T2">The type of the parameter2.</typeparam>
        /// <typeparam name="T3">The type of the parameter3.</typeparam>
        /// <typeparam name="T4">The type of the parameter4.</typeparam>
        /// <typeparam name="T5">The type of the parameter5.</typeparam>
        /// <typeparam name="T6">The type of the parameter6.</typeparam>
        /// <typeparam name="T7">The type of the parameter7.</typeparam>
        /// <typeparam name="T8">The type of the parameter8.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="parameter2">The parameter2.</param>
        /// <param name="parameter3">The parameter3.</param>
        /// <param name="parameter4">The parameter4.</param>
        /// <param name="parameter5">The parameter5.</param>
        /// <param name="parameter6">The parameter6.</param>
        /// <param name="parameter7">The parameter7.</param>
        /// <param name="parameter8">The parameter8.</param>
        /// <returns></returns>
        public static Action AsActionUsing<T1, T2, T3, T4, T5, T6, T7, T8>(this Action<T1, T2, T3, T4, T5, T6, T7, T8> action, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7, T8 parameter8)
        {
            return () => action(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8);
        }

        /// <summary>
        /// Extension method to expose a <see cref="System.Action{T1,T2,T3,T4,T5,T6,T7,T8,T9}" /> as an  <see cref="System.Action" />.
        /// </summary>
        /// <typeparam name="T1">The type of the parameter1.</typeparam>
        /// <typeparam name="T2">The type of the parameter2.</typeparam>
        /// <typeparam name="T3">The type of the parameter3.</typeparam>
        /// <typeparam name="T4">The type of the parameter4.</typeparam>
        /// <typeparam name="T5">The type of the parameter5.</typeparam>
        /// <typeparam name="T6">The type of the parameter6.</typeparam>
        /// <typeparam name="T7">The type of the parameter7.</typeparam>
        /// <typeparam name="T8">The type of the parameter8.</typeparam>
        /// <typeparam name="T9">The type of the parameter9.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="parameter2">The parameter2.</param>
        /// <param name="parameter3">The parameter3.</param>
        /// <param name="parameter4">The parameter4.</param>
        /// <param name="parameter5">The parameter5.</param>
        /// <param name="parameter6">The parameter6.</param>
        /// <param name="parameter7">The parameter7.</param>
        /// <param name="parameter8">The parameter8.</param>
        /// <param name="parameter9">The parameter9.</param>
        /// <returns></returns>
        public static Action AsActionUsing<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7, T8 parameter8, T9 parameter9)
        {
            return () => action(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8, parameter9);
        }

        /// <summary>
        /// Extension method to expose a <see cref="System.Action{T1,T2,T3,T4,T5,T6,T7,T8,T9,T10}" /> as an  <see cref="System.Action" />.
        /// </summary>
        /// <typeparam name="T1">The type of the parameter1.</typeparam>
        /// <typeparam name="T2">The type of the parameter2.</typeparam>
        /// <typeparam name="T3">The type of the parameter3.</typeparam>
        /// <typeparam name="T4">The type of the parameter4.</typeparam>
        /// <typeparam name="T5">The type of the parameter5.</typeparam>
        /// <typeparam name="T6">The type of the parameter6.</typeparam>
        /// <typeparam name="T7">The type of the parameter7.</typeparam>
        /// <typeparam name="T8">The type of the parameter8.</typeparam>
        /// <typeparam name="T9">The type of the parameter9.</typeparam>
        /// <typeparam name="T10">The type of the parameter10.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="parameter2">The parameter2.</param>
        /// <param name="parameter3">The parameter3.</param>
        /// <param name="parameter4">The parameter4.</param>
        /// <param name="parameter5">The parameter5.</param>
        /// <param name="parameter6">The parameter6.</param>
        /// <param name="parameter7">The parameter7.</param>
        /// <param name="parameter8">The parameter8.</param>
        /// <param name="parameter9">The parameter9.</param>
        /// <param name="parameter10">The parameter10.</param>
        /// <returns></returns>
        public static Action AsActionUsing<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7, T8 parameter8, T9 parameter9, T10 parameter10)
        {
            return () => action(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8, parameter9, parameter10);
        }

        /// <summary>
        /// Extension method to expose a <see cref="System.Action{T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11}" /> as an  <see cref="System.Action" />.
        /// </summary>
        /// <typeparam name="T1">The type of the parameter1.</typeparam>
        /// <typeparam name="T2">The type of the parameter2.</typeparam>
        /// <typeparam name="T3">The type of the parameter3.</typeparam>
        /// <typeparam name="T4">The type of the parameter4.</typeparam>
        /// <typeparam name="T5">The type of the parameter5.</typeparam>
        /// <typeparam name="T6">The type of the parameter6.</typeparam>
        /// <typeparam name="T7">The type of the parameter7.</typeparam>
        /// <typeparam name="T8">The type of the parameter8.</typeparam>
        /// <typeparam name="T9">The type of the parameter9.</typeparam>
        /// <typeparam name="T10">The type of the parameter10.</typeparam>
        /// <typeparam name="T11">The type of the parameter11.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="parameter2">The parameter2.</param>
        /// <param name="parameter3">The parameter3.</param>
        /// <param name="parameter4">The parameter4.</param>
        /// <param name="parameter5">The parameter5.</param>
        /// <param name="parameter6">The parameter6.</param>
        /// <param name="parameter7">The parameter7.</param>
        /// <param name="parameter8">The parameter8.</param>
        /// <param name="parameter9">The parameter9.</param>
        /// <param name="parameter10">The parameter10.</param>
        /// <param name="parameter11">The parameter11.</param>
        /// <returns></returns>
        public static Action AsActionUsing<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7, T8 parameter8, T9 parameter9, T10 parameter10, T11 parameter11)
        {
            return () => action(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8, parameter9, parameter10, parameter11);
        }

        /// <summary>
        /// Extension method to expose a <see cref="System.Action{T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12}" /> as an  <see cref="System.Action" />.
        /// </summary>
        /// <typeparam name="T1">The type of the parameter1.</typeparam>
        /// <typeparam name="T2">The type of the parameter2.</typeparam>
        /// <typeparam name="T3">The type of the parameter3.</typeparam>
        /// <typeparam name="T4">The type of the parameter4.</typeparam>
        /// <typeparam name="T5">The type of the parameter5.</typeparam>
        /// <typeparam name="T6">The type of the parameter6.</typeparam>
        /// <typeparam name="T7">The type of the parameter7.</typeparam>
        /// <typeparam name="T8">The type of the parameter8.</typeparam>
        /// <typeparam name="T9">The type of the parameter9.</typeparam>
        /// <typeparam name="T10">The type of the parameter10.</typeparam>
        /// <typeparam name="T11">The type of the parameter11.</typeparam>
        /// <typeparam name="T12">The type of the parameter12.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="parameter2">The parameter2.</param>
        /// <param name="parameter3">The parameter3.</param>
        /// <param name="parameter4">The parameter4.</param>
        /// <param name="parameter5">The parameter5.</param>
        /// <param name="parameter6">The parameter6.</param>
        /// <param name="parameter7">The parameter7.</param>
        /// <param name="parameter8">The parameter8.</param>
        /// <param name="parameter9">The parameter9.</param>
        /// <param name="parameter10">The parameter10.</param>
        /// <param name="parameter11">The parameter11.</param>
        /// <param name="parameter12">The parameter12.</param>
        /// <returns></returns>
        public static Action AsActionUsing<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7, T8 parameter8, T9 parameter9, T10 parameter10, T11 parameter11, T12 parameter12)
        {
            return () => action(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8, parameter9, parameter10, parameter11, parameter12);
        }

        /// <summary>
        /// Extension method to expose a <see cref="System.Action{T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13}" /> as an  <see cref="System.Action" />.
        /// </summary>
        /// <typeparam name="T1">The type of the parameter1.</typeparam>
        /// <typeparam name="T2">The type of the parameter2.</typeparam>
        /// <typeparam name="T3">The type of the parameter3.</typeparam>
        /// <typeparam name="T4">The type of the parameter4.</typeparam>
        /// <typeparam name="T5">The type of the parameter5.</typeparam>
        /// <typeparam name="T6">The type of the parameter6.</typeparam>
        /// <typeparam name="T7">The type of the parameter7.</typeparam>
        /// <typeparam name="T8">The type of the parameter8.</typeparam>
        /// <typeparam name="T9">The type of the parameter9.</typeparam>
        /// <typeparam name="T10">The type of the parameter10.</typeparam>
        /// <typeparam name="T11">The type of the parameter11.</typeparam>
        /// <typeparam name="T12">The type of the parameter12.</typeparam>
        /// <typeparam name="T13">The type of the parameter13.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="parameter2">The parameter2.</param>
        /// <param name="parameter3">The parameter3.</param>
        /// <param name="parameter4">The parameter4.</param>
        /// <param name="parameter5">The parameter5.</param>
        /// <param name="parameter6">The parameter6.</param>
        /// <param name="parameter7">The parameter7.</param>
        /// <param name="parameter8">The parameter8.</param>
        /// <param name="parameter9">The parameter9.</param>
        /// <param name="parameter10">The parameter10.</param>
        /// <param name="parameter11">The parameter11.</param>
        /// <param name="parameter12">The parameter12.</param>
        /// <param name="parameter13">The parameter13.</param>
        /// <returns></returns>
        public static Action AsActionUsing<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7, T8 parameter8, T9 parameter9, T10 parameter10, T11 parameter11, T12 parameter12, T13 parameter13)
        {
            return () => action(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8, parameter9, parameter10, parameter11, parameter12, parameter13);
        }

        /// <summary>
        /// Extension method to expose a <see cref="System.Action{T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14}" /> as an  <see cref="System.Action" />.
        /// </summary>
        /// <typeparam name="T1">The type of the parameter1.</typeparam>
        /// <typeparam name="T2">The type of the parameter2.</typeparam>
        /// <typeparam name="T3">The type of the parameter3.</typeparam>
        /// <typeparam name="T4">The type of the parameter4.</typeparam>
        /// <typeparam name="T5">The type of the parameter5.</typeparam>
        /// <typeparam name="T6">The type of the parameter6.</typeparam>
        /// <typeparam name="T7">The type of the parameter7.</typeparam>
        /// <typeparam name="T8">The type of the parameter8.</typeparam>
        /// <typeparam name="T9">The type of the parameter9.</typeparam>
        /// <typeparam name="T10">The type of the parameter10.</typeparam>
        /// <typeparam name="T11">The type of the parameter11.</typeparam>
        /// <typeparam name="T12">The type of the parameter12.</typeparam>
        /// <typeparam name="T13">The type of the parameter13.</typeparam>
        /// <typeparam name="T14">The type of the parameter14.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="parameter2">The parameter2.</param>
        /// <param name="parameter3">The parameter3.</param>
        /// <param name="parameter4">The parameter4.</param>
        /// <param name="parameter5">The parameter5.</param>
        /// <param name="parameter6">The parameter6.</param>
        /// <param name="parameter7">The parameter7.</param>
        /// <param name="parameter8">The parameter8.</param>
        /// <param name="parameter9">The parameter9.</param>
        /// <param name="parameter10">The parameter10.</param>
        /// <param name="parameter11">The parameter11.</param>
        /// <param name="parameter12">The parameter12.</param>
        /// <param name="parameter13">The parameter13.</param>
        /// <param name="parameter14">The parameter14.</param>
        /// <returns></returns>
        public static Action AsActionUsing<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7, T8 parameter8, T9 parameter9, T10 parameter10, T11 parameter11, T12 parameter12, T13 parameter13, T14 parameter14)
        {
            return () => action(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8, parameter9, parameter10, parameter11, parameter12, parameter13, parameter14);
        }

        /// <summary>
        /// Extension method to expose a <see cref="System.Action{T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15}" /> as an  <see cref="System.Action" />.
        /// </summary>
        /// <typeparam name="T1">The type of the parameter1.</typeparam>
        /// <typeparam name="T2">The type of the parameter2.</typeparam>
        /// <typeparam name="T3">The type of the parameter3.</typeparam>
        /// <typeparam name="T4">The type of the parameter4.</typeparam>
        /// <typeparam name="T5">The type of the parameter5.</typeparam>
        /// <typeparam name="T6">The type of the parameter6.</typeparam>
        /// <typeparam name="T7">The type of the parameter7.</typeparam>
        /// <typeparam name="T8">The type of the parameter8.</typeparam>
        /// <typeparam name="T9">The type of the parameter9.</typeparam>
        /// <typeparam name="T10">The type of the parameter10.</typeparam>
        /// <typeparam name="T11">The type of the parameter11.</typeparam>
        /// <typeparam name="T12">The type of the parameter12.</typeparam>
        /// <typeparam name="T13">The type of the parameter13.</typeparam>
        /// <typeparam name="T14">The type of the parameter14.</typeparam>
        /// <typeparam name="T15">The type of the parameter15.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="parameter2">The parameter2.</param>
        /// <param name="parameter3">The parameter3.</param>
        /// <param name="parameter4">The parameter4.</param>
        /// <param name="parameter5">The parameter5.</param>
        /// <param name="parameter6">The parameter6.</param>
        /// <param name="parameter7">The parameter7.</param>
        /// <param name="parameter8">The parameter8.</param>
        /// <param name="parameter9">The parameter9.</param>
        /// <param name="parameter10">The parameter10.</param>
        /// <param name="parameter11">The parameter11.</param>
        /// <param name="parameter12">The parameter12.</param>
        /// <param name="parameter13">The parameter13.</param>
        /// <param name="parameter14">The parameter14.</param>
        /// <param name="parameter15">The parameter15.</param>
        /// <returns></returns>
        public static Action AsActionUsing<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7, T8 parameter8, T9 parameter9, T10 parameter10, T11 parameter11, T12 parameter12, T13 parameter13, T14 parameter14, T15 parameter15)
        {
            return () => action(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8, parameter9, parameter10, parameter11, parameter12, parameter13, parameter14, parameter15);
        }

        /// <summary>
        /// Extension method to expose a <see cref="System.Action{T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16}" /> as an  <see cref="System.Action" />.
        /// </summary>
        /// <typeparam name="T1">The type of the parameter1.</typeparam>
        /// <typeparam name="T2">The type of the parameter2.</typeparam>
        /// <typeparam name="T3">The type of the parameter3.</typeparam>
        /// <typeparam name="T4">The type of the parameter4.</typeparam>
        /// <typeparam name="T5">The type of the parameter5.</typeparam>
        /// <typeparam name="T6">The type of the parameter6.</typeparam>
        /// <typeparam name="T7">The type of the parameter7.</typeparam>
        /// <typeparam name="T8">The type of the parameter8.</typeparam>
        /// <typeparam name="T9">The type of the parameter9.</typeparam>
        /// <typeparam name="T10">The type of the parameter10.</typeparam>
        /// <typeparam name="T11">The type of the parameter11.</typeparam>
        /// <typeparam name="T12">The type of the parameter12.</typeparam>
        /// <typeparam name="T13">The type of the parameter13.</typeparam>
        /// <typeparam name="T14">The type of the parameter14.</typeparam>
        /// <typeparam name="T15">The type of the parameter15.</typeparam>
        /// <typeparam name="T16">The type of the parameter16.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="parameter2">The parameter2.</param>
        /// <param name="parameter3">The parameter3.</param>
        /// <param name="parameter4">The parameter4.</param>
        /// <param name="parameter5">The parameter5.</param>
        /// <param name="parameter6">The parameter6.</param>
        /// <param name="parameter7">The parameter7.</param>
        /// <param name="parameter8">The parameter8.</param>
        /// <param name="parameter9">The parameter9.</param>
        /// <param name="parameter10">The parameter10.</param>
        /// <param name="parameter11">The parameter11.</param>
        /// <param name="parameter12">The parameter12.</param>
        /// <param name="parameter13">The parameter13.</param>
        /// <param name="parameter14">The parameter14.</param>
        /// <param name="parameter15">The parameter15.</param>
        /// <param name="parameter16">The parameter16.</param>
        /// <returns></returns>
        public static Action AsActionUsing<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> action, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7, T8 parameter8, T9 parameter9, T10 parameter10, T11 parameter11, T12 parameter12, T13 parameter13, T14 parameter14, T15 parameter15, T16 parameter16)
        {
            return () => action(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8, parameter9, parameter10, parameter11, parameter12, parameter13, parameter14, parameter15, parameter16);
        }

        /// <summary>
        /// Extension method to expose a <see cref="System.Action" /> as an  <see cref="System.Func{Unit}" />.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns>A <see cref="System.Func{Unit}" /></returns>
        public static Func<Unit> AsFunc(this Action action)
        {
            return ()=> { action(); return Unit.Default; };
        }
    }
}
