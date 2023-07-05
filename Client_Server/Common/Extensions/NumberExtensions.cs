namespace Common.Extensions.Number
{
    public static class NumberExtensions
    {
        public static ushort AddOne(this ushort number)
            => (ushort)(number + 1);

        public static int PickSmaller(this int numberOne, int numberTwo) => numberOne > numberTwo ? numberTwo : numberOne;

        public static int PickGreater(this int numberOne, int numberTwo) => numberOne > numberTwo ? numberOne : numberTwo;
    }
}
