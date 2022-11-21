namespace KATA_RPN
{
    public static class Stack
    {
        // Notre stack d'entiers
        public static Stack<int> stack = new();
        public static int StackCount()
        {
            return stack.Count;
        }

        public static Stack<int> AddValue(int newValue)
        {
            stack.Push(newValue);
            return stack;
        }
        // Prendre les deux dernières valeurs de la stack et les supprimer
        public static (int, int) TakeLastTwoValues()
        {
            int last = stack.Pop();
            int penultimate = stack.Pop();
            return (last, penultimate);
        }
        // Vider la stack
        public static Stack<int> FlushStack()
        {
            stack = new();
            return stack;
        }

        public static Stack<int> GetStack()
        {
            return stack;
        }
    }
}
