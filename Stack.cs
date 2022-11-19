namespace KATA_RPN
{
    public static class Stack
    {
        // Notre stack d'entiers
        public static List<int> stack = new();

        public static List<int> AddValue(int newValue)
        {
            stack.Add(newValue);
            return stack;
        }
        // Prendre les deux dernières valeurs de la stack et les supprimer
        public static (int, int) TakeLastTwoValues()
        {
            int last = stack.Last();
            stack.Remove(last);
            int penultimate = stack.Last();
            stack.Remove(penultimate);
            return (last, penultimate);
        }
        // Vider la stack
        public static List<int> FlushStack()
        {
            stack = new();
            return stack;
        }
    }
}
