using System;
using HSNXT.SuccincT.Functional;
using HSNXT.SuccincT.Options;
using HSNXT.SuccincT.Parsers;
using HSNXT.SuccincT.PatternMatchers.GeneralMatcher;

namespace HSNXT.SuccincT.Examples
{
    public static class UserInputExamples
    {
        public static Option<int> GetNumberFromUser(Action askMethod,
                                                    Action reAskMethod,
                                                    Func<string> getValueMethod,
                                                    int minValue,
                                                    int maxValue,
                                                    int numberOfAttempts)
        {
            var askAgain = TypedLambdas.Func<Action, Action, Func<string>, int, int, int, Option<int>>(GetNumberFromUser)
                .Apply(reAskMethod, reAskMethod, getValueMethod, minValue, maxValue);

            askMethod();
            return getValueMethod()
                .TryParseInt()
                .Match<Option<int>>()
                .Some().Where(i => i >= minValue && i <= maxValue).Do(Option<int>.Some)
                .Else(_ => numberOfAttempts.Match().To<Option<int>>()
                                           .Where(i => i <= 1).Do(Option<int>.None())
                                           .Else(__ => askAgain(--numberOfAttempts))
                                           .Result())
                .Result();
        }
    }
}