namespace MyProject
{
    using Godot;
    using Godot.Collections;

    using SabishiDev.ExportForge;

    [Tool]
    public partial class Example : Node
    {
        public int SomeInt { get; set; }
        public string SomeString { get; set; } = string.Empty;
        public Vector4 SomeVector4 { get; set; }
        public bool SomeBool { get; set; } = true;
        public FlagsExample SomeFlags { get; set; }

        public readonly string SomeReadOnlyString = "This is a read-only string.";

        private readonly EditorExportForge _forge;

        public Example()
        {
            _forge = new EditorExportForge(this);

            _forge
                .CreateProperty<bool>("Some Bool")
                .OnGet(() => SomeBool)
                .OnSet(value => SomeBool = value);

            // Create a property for the integer variable.
            _forge
                .CreateProperty<int>("Some Integer")
                .OnGet(() => SomeInt)
                .OnSet(value => SomeInt = value)
                .Range(0, 100, 5, orGreater: true, suffix: " units");

            // Create a property for the string variable.
            _forge
                .CreateProperty<string>("Some String")
                .OnGet(() => SomeString)
                .OnSet(value => SomeString = value)
                .Multiline();

            // Create a property for the vector variables.
            _forge
                .CreateProperty<Vector4>("Some Vector4")
                .OnGet(() => SomeVector4)
                .OnSet(value => SomeVector4 = value)
                .Range(0, 100, 2);

            // Create a tool button from callable property.
            _forge
                .CreateProperty<Callable>("Say Hello Button")
                .OnGet(() => Callable.From(SayHello))
                .ToolButton("Click Me", icon: "Variant");

            // Create a read-only string property.
            _forge
                .CreateProperty<string>("Read Only String")
                .OnGet(() => SomeReadOnlyString)
                .ReadOnly();

            // Create a property that is shown only when a certain condition is true.
            _forge
                .CreateProperty<Callable>("Conditional Action Button")
                .When(() => SomeInt == 10)
                .OnGet(() => Callable.From(ConditionalAction))
                .ToolButton("Conditional Action", icon: "Variant");

            // Create a flags property.
            _forge
                .CreateProperty<int>("Some Flags")
                .OnGet(() => (int)SomeFlags)
                .OnSet(value => SomeFlags = (FlagsExample)value)
                .Flags<FlagsExample>();
        }

        public override Array<Dictionary> _GetPropertyList()
        {
            return _forge.ForgeProperties();
        }

        public override bool _Set(StringName property, Variant value)
        {
            return _forge.HandleSetter(property, value);
        }

        public override Variant _Get(StringName property)
        {
            return _forge.HandleGetter(property);
        }

        private void SayHello()
        {
            GD.Print("Hello from Example!");
        }

        private void ConditionalAction()
        {
            GD.Print("Conditional Action.");
        }
    }
}
