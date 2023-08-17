namespace MyUnit
{
    public static class MyAssert
    {
        public static void True(bool actual)
        {
            if (!actual)
                throw new MyAssertTestFailureException();
        }
    }
}
