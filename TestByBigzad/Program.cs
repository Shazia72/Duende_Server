using System.Linq;

namespace TestByBigzad
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            List<int> Nums = new List<int> { 2, 6, 1, 3, 2, 7 };
            int[] arr1 = new int[] { 1, 2, 2, 3, 3, 4, 6,6,7, 7,8,8,9,10};
            int val = 4;
            //int[] arr = Container.findTwoSum(Nums, val);
            //Console.WriteLine(String.Join(",", arr));

            int result = RemoveDuplicateFromArray(arr1);
            Console.WriteLine($"duplicate: {result}");

            bool palindrom = Palindrom(2345);
            Console.WriteLine($"duplicate: {palindrom}");
        }

        //public static class Container
        //{
        //    public static int[] findTwoSum(List<int> nums, int target)
        //    {
        //        var dic = new Dictionary<int, int>();
  
        //        for (int i = 0; i < nums.Count; i++)
        //        {
        //            if (dic.ContainsKey(nums[i]))
        //            {
        //                return new int[] { dic[nums[i]], i };

        //            }
        //            else
        //            {
        //                dic[target - nums[i]] = i;
        //            }
        //        }

        //        return new int[] { };
        //    }
        //}
        public static int RemoveDuplicateFromArray(int[] arr)// sorted array
        {
            int i = 0;
            List<int> list = new List<int>() { arr[i] };
            for (int j = 1; j < arr.Length; j++)
            {
                if (arr[i] != arr[j])
                {  
                    i++;  // if 2 values are not same then increment i
                    arr[i] = arr[j]; // move value from j pointer to value at pointer i, since i does update
                                     // when duplicate value found then move value from j to i, hence removing the duplicate value 
                    list.Add(arr[j]);
                }
            }
            return i + 1; // no of uniques elements
        }

        public static bool Palindrom(int x)
        {
            int reverse = 0;
            int temp = x; // let x=2345;
            while (temp > 0)
            {
                int r = temp % 10; // to remove last digit from input, r= 5
                reverse = reverse * 10 + r; // reverse was=0 now + r= reverse=5
                temp = temp / 10; // to remove last digit as we alraedy checked the last digit, its is reverse
            }
            if (x == reverse) return true;
            else return false;
        }
        public double findMedianSortedArrays(int[] arr1, int[] arr2)
        {
            int l1 = arr1.Length; // length of arr1
            int l2 = arr2.Length; // length of arr2
            int l = l1 + l2;  // total length for final array

            int[] result = new int[l]; // result array to store combined array

            int i = 0, j = 0, k = 0;

            while (i <= l1 && j <= l2)
            {
                if (i == l1)  // if arr1 length=0
                {
                    while (j < l2)
                        result[k++] = arr2[j++]; // keeps on looping for arr2 and save in final array
                    break;
                }
                else if (j == l2) //if arr2 length=0
                { 
                    while (i < l1)
                        result[k++] = arr1[i++]; // keeps on looping for arr1 and save in final array
                    break;
                }
                // if any one array length is greater than zero then it will compare the 2 arrays
                if (arr1[i] < arr2[j])
                {
                    result[k++] = arr1[i++];
                }
                else
                {
                    result[k++] = arr2[j++];
                }
            }
            if (l % 2 == 0) return (float) (  result[l/2 - 1] + result[l/2]    )/2;
            else return result[l / 2];
        }

        public int getMaxDigit(int num)
        {
            int maxDigit = 0;
            while (num > 0)
            {
                int lastDigit = num % 10;
                num /= 10;
                if (maxDigit < lastDigit)
                {
                    maxDigit = lastDigit;
                }
            }
            return maxDigit;
        }
        public int maxSum(int[] nums)// which 2 numbers give max sum in an array
        {
            Dictionary<int, int> map = new Dictionary<int, int>();
            int ans = -1;
            for (int i = 0; i < nums.Length; i++)
            {
                int currAns = getMaxDigit(nums[i]);
                if (map.ContainsKey(currAns))
                {
                    if (nums[i] + map[currAns] > ans)
                    {
                        ans = nums[i] + map[currAns];
                    }
                    if (nums[i] > map[currAns])
                    {
                        //map.put(currAns, nums[i]);
                        map[currAns] = nums[i];
                    }
                }
                else
                {
                    //map.put(currAns, nums[i]);
                    map[currAns] = nums[i];
                }
            }
            return ans;
        }
    }
}