namespace ViewModels.ViewModels
{
    public class WidgetDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string? IconPath { get; set; }
        public string? Design { get; set; }
        public string? CodeSnippet { get; set; }
        public string? LayoutDescription { get; set; }
        public string? LayoutIconPath { get; set; }
        public int? ParentWidgetID { get; set; }

        public int RelatedAppTypeID { get; set; }
        public bool IsOnlyNested { get; set; }

    }
}
