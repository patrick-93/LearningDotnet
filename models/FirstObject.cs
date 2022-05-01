namespace DotnetClient
{
    public class FirstObject
    {
        public Guid Id { get; }
        public string Owner { get; set; }

        public int Items { get; set; }

        public FirstObject(string owner, int items)
        {
            this.Id = Guid.NewGuid();
            this.Owner = owner;
            this.Items = items;
        }

        public override string ToString()
        {
            return "FirstObject[ID=" + this.Id + ", Owner=" + this.Owner + ", Items=" + this.Items + "]";
        }
    }
}