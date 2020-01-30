# Xamarin forms

## Data binding

```xaml
<StackLayout HorizontalOptions="Center" VerticalOptions="Center">
	<Label Text="{Binding 
                 Source={x:Reference slider}, 
                 Path=Value, 
                 StringFormat='Value is {0:F2}'}" 
           Opacity="{Binding
                    Source={x:Reference slider},
                    Path=Value}"/>
    <Slider x:Name="slider"/>
</StackLayout>
```

or

 ```xaml
<StackLayout HorizontalOptions="Center" VerticalOptions="Center">
	<Label BindingContext="{x:Reference slider}"
           Text="{Binding Value, StringFormat='Value is {0:F2}'}" 
           Opacity="{Binding Value}"/>
    <Slider x:Name="slider"/>
</StackLayout>
 ```

or

```xaml
<StackLayout BindingContext="{x:Reference slider}" 
             HorizontalOptions="Center" VerticalOptions="Center">
    <BoxView Color="Green" Opacity="{Binding Value}"/>
	<Label Text="{Binding Value, StringFormat='Value is {0:F2}'}" 
           Opacity="{Binding Value}"/>
    <Slider x:Name="slider"/>
</StackLayout>
```

## Dealing with Device Differences

```c#
public MainPage() {
	InitializeComponent();
	Padding = Device.OnPlatform(
    	iOS: new Thickness(0, 20, 0, 0),
        Android: new Thickness(10, 20, 0, 0),
        WinPhone: new Thickness(30, 20, 0, 0),
    )
}
```

or

```c#
public MainPage() {
    InitializeComponent();
    if (Device.OS == TargetPlatform.iOS)
        Padding = new Thickness(0, 20, 0, 0);
}
```

## Property Element Syntax???

```xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="HelloWorld.MainPage">
    
    <ContentPage.Padding>
    	<OnPlatform x:TypeArguments="Thinkness" 
                    iOS="0,20,0,0" 
                    Anroid="0,40,0,0">
        </OnPlatform>
    </ContentPage.Padding>

    <StackLayout BindingContext="{x:Reference slider}" 
                 HorizontalOptions="Center" VerticalOptions="Center">
        <BoxView Color="Green" Opacity="{Binding Value}"/>
        <Label Text="{Binding Value, StringFormat='Value is {0:F2}'}" 
               Opacity="{Binding Value}"/>
        <Slider x:Name="slider"/>
    </StackLayout>
    
</ContentPage>

```

## Stack Layout

```xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="HelloWorld.MainPage">
    
	<StackLayout Orientation="Horizontal"
                 Padding="40" Spacing="20"
                 VerticalOptions="Center"
                 HorizontalOptions="Center">
		<StackLayout>
            <Image Source="http://placehold.it/100x100"/>
            <Label Text="Label 1"/>
        </StackLayout>
        <Label Text="Label 2"/>
        <Label Text="Label 3"/>
    </StackLayout>

</ContentPage>
```

<img src="F:\notes\xamarin_forms\StackLayout.png" style="zoom:25%;" />

## Stack Layout in Code

```c #
public MainPage() {
    InitializeComponent();
    var layout = new StackLayout {
        Spacing = 40,
        Padding = new Thickness(0,20,0,0),
        Orientation = StackOrientation.Horizontal
    }
    layout.Children.Add(new Label {Text = "Label 1"});
    Content = layout;
}
```

## Grid Layout

```xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="HelloWorld.MainPage">
    
	<Grid BackgroundColor="Yellow" 
          RowSpacing="40"
          ColumnSpacing="40">
    	<Grid.RowDefinitions>
        	<RowDefinition Height="100"/>
            <RowDefinition Height="2*"/>
            <RowDefinition Height="*"/>
        </Grid.RowDefinitions>
        
        <Label Grid.Row="0" Grid.Column="0" Text="Label 1" BackgroundColor="Silver"/>
        <Label Grid.Row="0" Grid.Column="1" Text="Label 2" BackgroundColor="Silver"/>
        <Label Grid.Row="1" Grid.Column="0" Text="Label 3" BackgroundColor="Silver"/>
        <Label Grid.Row="1" Grid.Column="1" Text="Label 4" BackgroundColor="Silver"/>
        <Label Grid.Row="2" 
               Grid.ColumnSpan="3" 
               Text="ColumnSpan" 
               BackgroundColor="Silver"/>
        <Label Grid.Column="2" 
               Grid.RowSpan="3" 
               Text="RowSpan" 
               BackgroundColor="Silver"/>
    </Grid>

</ContentPage>
```

<img src="F:\notes\xamarin_forms\GridLayout.png" style="zoom:25%;" />

## Grid Layout in Code

```c#
public MainPage() {
    InitializeComponent();
    var grid = new Grid {RowSpacing = 20, ColumnSpacing = 40};
    var label = new Label {Text = "Label 1"};
    grid.Children.Add(label, 0, 0);
    Grid.SetRowSpan(label, 2);
    Grid.SetColumnSpan(label, 2);
    grid.RowDefinitions.Add(new Rowdefinition {
        Height = new GridLength(100, GridUnitType.Absolute)
    });
    grid.RowDefinitions.Add(new Rowdefinition {
        Height = new GridLength(1, GridUnitType.Star)
    });
    grid.RowDefinitions.Add(new Rowdefinition {
        Height = new GridLength(2, GridUnitType.Star)
    });
    grid.RowDefinitions.Add(new Rowdefinition {
        Height = new GridLength(1, GridUnitType.Auto)
    });
}
```

## Absolute Layout

```xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="HelloWorld.MainPage">
    
	<AbsoluteLayout>
    	<BoxView Color="Aqua"
                 AbsoluteLayout.LayoutBounds="0, 0, 1, 1" 
                 AbsoluteLayout.LayoutFlags="All"/>
        <BoxView Color="White"
                 AbsoluteLayout.LayoutBounds="0.5, 0.1, 100, 100" 
                 AbsoluteLayout.LayoutFlag="PositionProportional"/>
        <BoxView Text="Get Started"
                 BackgroundColor="Silver"
                 TextColor="White"
                 AbsoluteLayout.LayoutBounds="0, 1, 1, 50"
                 AbsoluteLayout.LayoutFlags="PositionProportional, WidthProportional"/>
    </AbsoluteLayout>

</ContentPage>
```

<img src="F:\notes\xamarin_forms\AbsoluteLayout.png" style="zoom:25%;" />