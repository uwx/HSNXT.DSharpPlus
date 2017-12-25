using System;

namespace HSNXT
{
    public partial class Extensions
    {
        /// <summary>
        /// Extension method to expose a <see cref="System.Func{T}"/> as an <see cref="System.Action"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static Action AsAction<T>(this Func<T> function)
        {
            return () => function();
        }

        /// <summary>
        /// Extension method to expose a <see cref="System.Func{T,TResult}"/> as an  <see cref="System.Action"/>.
        /// </summary>
        /// <typeparam name="TResult">The type of the output.</typeparam>
        /// <typeparam name="T">The type of the parameter.</typeparam>
        /// <param name="function">The function.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        public static Action AsActionUsing<T, TResult>(this Func<T, TResult> function, T parameter)
        {
            return () => function(parameter);
        }

        /// <summary>
        /// Extension method to expose a <see cref="System.Func{T1,T2,TResult}"/> as an  <see cref="System.Action"/>.
        /// </summary>
        /// <typeparam name="TResult">The type of the output.</typeparam>
        /// <typeparam name="T1">The type of parameter1.</typeparam>
        /// <typeparam name="T2">The type of parameter2.</typeparam>
        /// <param name="function">The function.</param>
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="parameter2">The parameter2.</param>
        /// <returns></returns>
        public static Action AsActionUsing<T1, T2, TResult>(this Func<T1, T2, TResult> function, T1 parameter1, T2 parameter2)
        {
            return () => function(parameter1, parameter2);
        }


        /// <summary>
        /// Extension method to expose a <see cref="System.Func{T1,T2,T3,TResult}" /> as an  <see cref="System.Action" />.
        /// </summary>
        /// <typeparam name="TResult">The type of the output.</typeparam>
        /// <typeparam name="T1">The type of parameter1.</typeparam>
        /// <typeparam name="T2">The type of parameter2.</typeparam>
        /// <typeparam name="T3">The type of parameter3.</typeparam>
        /// <param name="function">The function.</param>
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="parameter2">The parameter2.</param>
        /// <param name="parameter3">The parameter3.</param>
        /// <returns></returns>
        public static Action AsActionUsing<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> function, T1 parameter1, T2 parameter2, T3 parameter3)
        {
            return () => function(parameter1, parameter2, parameter3);
        }
        /// <summary>
        /// Extension method to expose a <see cref="System.Func{T1,T2,T3,T4,TResult}" /> as an  <see cref="System.Action" />.
        /// </summary>
        /// <typeparam name="T1">The type of the parameter1.</typeparam>
        /// <typeparam name="T2">The type of the parameter2.</typeparam>
        /// <typeparam name="T3">The type of the parameter3.</typeparam>
        /// <typeparam name="T4">The type of the parameter4.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="function">The function.</param>
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="parameter2">The parameter2.</param>
        /// <param name="parameter3">The parameter3.</param>
        /// <param name="parameter4">The parameter4.</param>
        /// <returns></returns>
        public static Action AsActionUsing<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> function, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4)
        {
            return () => function(parameter1, parameter2, parameter3, parameter4);
        }
        /// <summary>
        /// Extension method to expose a <see cref="System.Func{T1,T2,T3,T4,T5,TResult}" /> as an  <see cref="System.Action" />.
        /// </summary>
        /// <typeparam name="T1">The type of the parameter1.</typeparam>
        /// <typeparam name="T2">The type of the parameter2.</typeparam>
        /// <typeparam name="T3">The type of the parameter3.</typeparam>
        /// <typeparam name="T4">The type of the parameter4.</typeparam>
        /// <typeparam name="T5">The type of the parameter5.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="function">The function.</param>
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="parameter2">The parameter2.</param>
        /// <param name="parameter3">The parameter3.</param>
        /// <param name="parameter4">The parameter4.</param>
        /// <param name="parameter5">The parameter5.</param>
        /// <returns></returns>
        public static Action AsActionUsing<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> function, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5)
        {
            return () => function(parameter1, parameter2, parameter3, parameter4, parameter5);
        }

        /// <summary>
        /// Extension method to expose a <see cref="System.Func{T1,T2,T3,T4,T5,T6,TResult}" /> as an  <see cref="System.Action" />.
        /// </summary>
        /// <typeparam name="T1">The type of the parameter1.</typeparam>
        /// <typeparam name="T2">The type of the parameter2.</typeparam>
        /// <typeparam name="T3">The type of the parameter3.</typeparam>
        /// <typeparam name="T4">The type of the parameter4.</typeparam>
        /// <typeparam name="T5">The type of the parameter5.</typeparam>
        /// <typeparam name="T6">The type of the parameter6.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="function">The function.</param>
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="parameter2">The parameter2.</param>
        /// <param name="parameter3">The parameter3.</param>
        /// <param name="parameter4">The parameter4.</param>
        /// <param name="parameter5">The parameter5.</param>
        /// <param name="parameter6">The parameter6.</param>
        /// <returns></returns>
        public static Action AsActionUsing<T1, T2, T3, T4, T5, T6, TResult>(this Func<T1, T2, T3, T4, T5, T6, TResult> function, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6)
        {
            return () => function(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6);
        }

        /// <summary>
        /// Extension method to expose a <see cref="System.Func{T1,T2,T3,T4,T5,T6,T7,TResult}" /> as an  <see cref="System.Action" />.
        /// </summary>
        /// <typeparam name="T1">The type of the parameter1.</typeparam>
        /// <typeparam name="T2">The type of the parameter2.</typeparam>
        /// <typeparam name="T3">The type of the parameter3.</typeparam>
        /// <typeparam name="T4">The type of the parameter4.</typeparam>
        /// <typeparam name="T5">The type of the parameter5.</typeparam>
        /// <typeparam name="T6">The type of the parameter6.</typeparam>
        /// <typeparam name="T7">The type of the parameter7.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="function">The function.</param>
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="parameter2">The parameter2.</param>
        /// <param name="parameter3">The parameter3.</param>
        /// <param name="parameter4">The parameter4.</param>
        /// <param name="parameter5">The parameter5.</param>
        /// <param name="parameter6">The parameter6.</param>
        /// <param name="parameter7">The parameter7.</param>
        /// <returns></returns>
        public static Action AsActionUsing<T1, T2, T3, T4, T5, T6, T7, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, TResult> function, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7)
        {
            return () => function(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7);
        }

        /// <summary>
        /// Extension method to expose a <see cref="System.Func{T1,T2,T3,T4,T5,T6,T7,T8,TResult}" /> as an  <see cref="System.Action" />.
        /// </summary>
        /// <typeparam name="T1">The type of the parameter1.</typeparam>
        /// <typeparam name="T2">The type of the parameter2.</typeparam>
        /// <typeparam name="T3">The type of the parameter3.</typeparam>
        /// <typeparam name="T4">The type of the parameter4.</typeparam>
        /// <typeparam name="T5">The type of the parameter5.</typeparam>
        /// <typeparam name="T6">The type of the parameter6.</typeparam>
        /// <typeparam name="T7">The type of the parameter7.</typeparam>
        /// <typeparam name="T8">The type of the parameter8.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="function">The function.</param>
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="parameter2">The parameter2.</param>
        /// <param name="parameter3">The parameter3.</param>
        /// <param name="parameter4">The parameter4.</param>
        /// <param name="parameter5">The parameter5.</param>
        /// <param name="parameter6">The parameter6.</param>
        /// <param name="parameter7">The parameter7.</param>
        /// <param name="parameter8">The parameter8.</param>
        /// <returns></returns>
        public static Action AsActionUsing<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> function, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7, T8 parameter8)
        {
            return () => function(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8);
        }

        /// <summary>
        /// Extension method to expose a <see cref="System.Func{T1,T2,T3,T4,T5,T6,T7,T8,T9,TResult}" /> as an  <see cref="System.Action" />.
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
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="function">The function.</param>
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
        public static Action AsActionUsing<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> function, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7, T8 parameter8, T9 parameter9)
        {
            return () => function(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8, parameter9);
        }
        /// <summary>
        /// Extension method to expose a <see cref="System.Func{T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,TResult}" /> as an  <see cref="System.Action" />.
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
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="function">The function.</param>
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
        public static Action AsActionUsing<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> function, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7, T8 parameter8, T9 parameter9, T10 parameter10)
        {
            return () => function(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8, parameter9, parameter10);
        }

        /// <summary>
        /// Extension method to expose a <see cref="System.Func{T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,TResult}" /> as an  <see cref="System.Action" />.
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
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="function">The function.</param>
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
        public static Action AsActionUsing<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> function, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7, T8 parameter8, T9 parameter9, T10 parameter10, T11 parameter11)
        {
            return () => function(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8, parameter9, parameter10, parameter11);
        }

        /// <summary>
        /// Extension method to expose a <see cref="System.Func{T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,TResult}" /> as an  <see cref="System.Action" />.
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
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="function">The function.</param>
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
        public static Action AsActionUsing<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> function, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7, T8 parameter8, T9 parameter9, T10 parameter10, T11 parameter11, T12 parameter12)
        {
            return () => function(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8, parameter9, parameter10, parameter11, parameter12);
        }

        /// <summary>
        /// Extension method to expose a <see cref="System.Func{T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,TResult}" /> as an  <see cref="System.Action" />.
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
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="function">The function.</param>
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
        public static Action AsActionUsing<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> function, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7, T8 parameter8, T9 parameter9, T10 parameter10, T11 parameter11, T12 parameter12, T13 parameter13)
        {
            return () => function(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8, parameter9, parameter10, parameter11, parameter12, parameter13);
        }
        /// <summary>
        /// Extension method to expose a <see cref="System.Func{T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,TResult}" /> as an  <see cref="System.Action" />.
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
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="function">The function.</param>
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
        public static Action AsActionUsing<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> function, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7, T8 parameter8, T9 parameter9, T10 parameter10, T11 parameter11, T12 parameter12, T13 parameter13, T14 parameter14)
        {
            return () => function(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8, parameter9, parameter10, parameter11, parameter12, parameter13, parameter14);
        }
        /// <summary>
        /// Extension method to expose a <see cref="System.Func{T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,TResult}" /> as an  <see cref="System.Action" />.
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
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="function">The function.</param>
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
        public static Action AsActionUsing<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> function, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7, T8 parameter8, T9 parameter9, T10 parameter10, T11 parameter11, T12 parameter12, T13 parameter13, T14 parameter14, T15 parameter15)
        {
            return () => function(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8, parameter9, parameter10, parameter11, parameter12, parameter13, parameter14, parameter15);
        }

        /// <summary>
        /// Extension method to expose a <see cref="System.Func{T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,TResult}" /> as an  <see cref="System.Action" />.
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
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="function">The function.</param>
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
        public static Action AsActionUsing<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> function, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7, T8 parameter8, T9 parameter9, T10 parameter10, T11 parameter11, T12 parameter12, T13 parameter13, T14 parameter14, T15 parameter15, T16 parameter16)
        {
            return () => function(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8, parameter9, parameter10, parameter11, parameter12, parameter13, parameter14, parameter15, parameter16);
        }
    }
}
