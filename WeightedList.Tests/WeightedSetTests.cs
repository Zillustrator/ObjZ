using System.Collections.Generic;
using NUnit.Framework;

namespace ObjZ.Collections.Generic.Tests
{
    public class WeightedSetTests
    {
        [Test]
        public void SeededWeightedSetsArePredictableInRandomness()
        {
            var wSet1 = new WeightedSet<string>(100)
            {
                { "Apple", 5 },
                { "Banana", 10 },
                { "Orange", 20 },
            };

            var wSet2 = new WeightedSet<string>(100)
            {
                { "Apple", 5 },
                { "Banana", 10 },
                { "Orange", 20 },
            };

            for (int i = 0; i < 100; ++i)
            {
                Assert.AreEqual(wSet1.Random, wSet2.Random);
            }

            for (int i = 0; i < 100; ++i)
            {
                wSet1.Seed = i;
                wSet2.Seed = i;

                Assert.AreEqual(wSet1.Random, wSet2.Random);
            }
        }

        [Test]
        public void CallingRandomOnAWeightedSetWithZeroWeightThrowsWeightedSetException()
        {
            var weightedList = new WeightedSet<string>(100)
            {
                { "Apple", 0 },
                { "Banana", 0 },
                { "Orange", 0 },
            };

            Assert.Throws<WeightedSetException>(() => { _ = weightedList.Random; });
        }

        [Test]
        public void TotalWeightEqualsTheSumOfAllItemWeights()
        {
            var wSet = new WeightedSet<string>()
            {
                { "Apple", 5 },
                { "Banana", 10 },
                { "Orange", 20 },
            };

            Assert.AreEqual(35, wSet.Weight);
        }

        [Test]
        public void ZeroWeightedItemsNeverOccur()
        {
            var wSet = new WeightedSet<string>()
            {
                { "Apple", 0 },
                { "Banana", 0 },
                { "Orange", 1 },
            };

            for (int i = 0; i < 100; ++i)
            {
                Assert.AreEqual("Orange", wSet.Random);
            }
        }

        [Test]
        public void HeavierWeightedItemsOccurMoreFrequently()
        {
            var wSet = new WeightedSet<string>(100)
            {
                { "Apple", 1 },
                { "Banana", 2 },
            };

            var count = new Dictionary<string, int>()
            {
                { "Apple", 0 },
                { "Banana", 0 },
            };

            for (int i = 0; i < 100; ++i)
            {
                count[wSet.Random] += 1;
            }

            Assert.Greater(count["Banana"], count["Apple"]);

            wSet["Apple"] = 2;
            wSet["Banana"] = 1;

            count["Apple"] = 0;
            count["Banana"] = 0;

            for (int i = 0; i < 100; ++i)
            {
                count[wSet.Random] += 1;
            }

            Assert.Greater(count["Apple"], count["Banana"]);
        }
    }
}
