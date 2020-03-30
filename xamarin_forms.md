# Xamarin forms

## Basics

### Data binding

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

### Dealing with Device Differences

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

### Property Element Syntax???

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

## Layout

### Stack Layout

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

<img src="https://raw.githubusercontent.com/Nathan-Mu/notes/master/.img/xamarin_forms/StackLayout.png" style="zoom:25%;" />

### Stack Layout in Code

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

### Grid Layout

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

<img src="https://raw.githubusercontent.com/Nathan-Mu/notes/master/.img/xamarin_forms/GridLayout.png" style="zoom:25%;" />

### Grid Layout in Code

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

### Absolute Layout

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

<img src="https://raw.githubusercontent.com/Nathan-Mu/notes/master/.img/xamarin_forms/AbsoluteLayout.png" style="zoom:25%;" />

### Absolute Layout in Code

```c#
public MainPage() {
    InitializeComponent();
    var layout = new AbsoluteLayout();
    var aquaBox = new BoxView { Color = Color.Aqua };
    layout.Children.Add(aquaBox, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);
	Content = layout;
}
```

### Relative Layout

```xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="HelloWorld.MainPage">
    
	<RelativeLayout>
        <BoxView Color="Aqua" x:Name="banner"
                 RelativeLayout.WidthConstraint="{ConstraintExpression 
                                                 Type=RelativeToParent, 
                                                 Property=Width,
                                                 Factor=1}"
                 RelativeLayout.HeightConstraint="{ConstraintExpression 
                                                  Type=RelativeToParent, 
                                                  Property=Height,
                                                  Factor=0.3}" />
		<BoxView Color="Silver"
                 RelativeLayout.YConstraint="{ConstraintExpression
                                             Type=RelativeToView,
                                             ElementName=banner,
                                             Property=Height,
                                             Factor=1,
                                             Constant=20}"/>
    </RelativeLayout>

</ContentPage>
```

![](https://raw.githubusercontent.com/Nathan-Mu/notes/master/.img/xamarin_forms/RelativeLayout.PNG)

### Relative Layout in Code

```c#
public MainPage() {
    InitializeComponent();
    var layout = new RelativeLayout();
    var aquaBox = new BoxView { Color = Color.Aqua };
    layout.Children.Add(
        aquaBox,
        widthConstraint: Constraint.RelativeToParent(parent => parent.Width),
        heightConstraint: Constraint.RelativeToParent(parent => parent.Height));
    var silverBox = new BoxView { Color = Color.Silver };
    layout.Children.Add(
    	silverBox, 
        yConstraint: Constraint.RelativeToView(
            auqaBox, 
            (parent, aquaBox) => aquaBox.Height + 20);
    );
}
```

## Image

```xam
<Image Source="http://....." x:Name="image" Aspect="Fill"/>
```

```c#
// var imageSource = (UriImageSource) ImageSource.FromUri(new Uri("http://...."));
var imageSource = new UriImageSource { Uri = new Uri("http://...")};
image.Source = imageSource
```

or

```c#
image.Source = "http://...";
```

other properties

```c#
imageSource.CachingEnable = false;
imageSource.CacheValidity = TimeSpan.FromHours(1);
```

### Activity Indicator

```xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="HelloWorld.MainPage"
             BackgroundColor="Black">
    <AbsoluteLayout>
        <ActivityIndicator IsRunning="{Binding Source={x:Reference image}, Path=IsLoading}"
                           Color="White"
                           AbsoluteLayout.LayoutBounds="0.5, 0.5, 100, 100"
                           AbsoluteLayout.LayoutFlags="PositionPropotional"/>
        <Image x:Name="image" Aspect="AspectFill"
               AbsoluteLayout.LayoutBounds="0, 0, 1, 1"
               AbsoluteLayout.LayoutFlags="All"/>
    </AbsoluteLayout>
</ContentPage>
```

## List View

```xaml
<ListView x:Name="listView" SeparatorColor="Blue"/>
```

```c#
public MainPage() {
    InitializeComponent();
    var names = new List<string> {"Mosh", "John", "Bob"};
    listView.ItemsSource= names;
}
```

### Text Cell

```xaml
<ListView x:Name="ListView">
	<ListView.ItemTemplate>
    	<DataTemplate>
        	<TextCell Text="{Binding Name}" Detail="{Binding Status}"/>
        </DataTemplate>
    </ListView.ItemTemplate>
</ListView>
```

### Custom Cells

```xaml
<ListView x:Name="ListView" HasUnevenRows="true">
	<ListView.ItemTemplate>
    	<DataTemplate>
        	<ViewCell>
            	<StackLayout Orientation="Horizontal" Padding="5">
                	<Image Source="{Binding ImageUrl}"/>
                    <StackLayout HorizontalOptions="StartAndExpand">
                    	<Label Text="{Binding Name}"/>
                        <Label Text="{Binding Status}" TextColor="Gray"/>
                    </StackLayout>
                    <Button Text="Follow"/>
                </StackLayout>
            </ViewCell>
        </DataTemplate>
    </ListView.ItemTemplate>
</ListView>
```

### Grouping Items

```c#
public class ContactGroup : List<Contact> {
    public string Title { get; set; }
    public string ShortTile { get; set; }
    
    public ContactGroup(string title, string shortTitle) {
        Title = title;
        ShortTitle = shortTitle;
    }
}
```

```c#
public MainPage() {
    InitializeComponent();
    listView.ItemsSource = new List<ContactGroup> {
        new ContactGroup("Ms", "M") {
            new Contact { Name = "Mosh", ImageUrl = "..."}
        }, 
        new ContactGroup("Js", "J") {
            new Contact { Name = "John", ImageUrl = "...", Status = "..."}
        }
    };
}
```

```xaml
<ListView x:Name="listView" 
          IsGroupingEnable="true"
          GroupDisplayBinding="{Binding Title}"
          GroupShortNameBinding="{Binding ShortTitle}">
	<ListView.ItemTemplate>
    	<DataTemplate>
        	<TextCell Text="{Binding Name}" Detail="{Binding Status}"/>
        </DataTemplate>
    </ListView.ItemTemplate>
</ListView>
```

Handling Selection

```xaml
<ListView x:Name="listView" 
          ItemTapped="Handle_ItemTapped"
          ItemSelected="Handle_ItemSelected">
	<ListView.ItemTemplate>
    	<DataTemplate>
        	<TextCell Text="{Binding Name}" Detail="{Binding Status}"/>
        </DataTemplate>
    </ListView.ItemTemplate>
</ListView>
```

```c#
void Handle_ItemSelected(object sender, SelectedItemChangedEvent e) {
    var contact = e.SelectedItem as Contact;
    DisplayAlert("Selected", contact.Name, "OK");
}
void Handle_ItemTapped(object sender, ItemTappedEventArgs e) {
    var contact = e.Item as Contact;
    DisplayAlert("Tapped", contact.Name, "OK");
}
```

### Context Action

```xaml
<ListView x:Name="listView">
	<ListView.ItemTemplate>
    	<DataTemplate>
        	<TextCell Text="{Binding Name}" Detail="{Binding Status}">
            	<TextCell.ContextActions>
                	<MenuItem Text="Call" Clicked="Call_Clicked"
                              CommendParameter="{Binding .}"/>
                    <MenuItem Text="Delete" Clicked="Delete_Clicked" 
                              IsDestructive="true"
                              CommendParameter="{Binding .}"/>
                </TextCell.ContextActions>
            </TextCell>
        </DataTemplate>
    </ListView.ItemTemplate>
</ListView>
```

```c#
private ObservableCollection<Contact> _contacts;

void Delete_Clicked(object sender, System.EventArgs e) {
    var contact = (sender as MenuItem).CommandParameter as Contact;
    _contacts.Remove(contact);
}

void Call_Clicked(object sender, System.EventArgs e) {
    var menuItem = sender as MenuItems;
    var contact = menuItem.CommandParameter as Contact;
    DisplayAlert("Call", contact.Name, "OK");
}
```

### Pull to Refresh

```xaml
<ListView x:Name="listView" IsPullToRefreshEnabled="true" Refreshing="Handle_Refreshing">
	<ListView.ItemTemplate>
    	<DataTemplate>
        	<TextCell Text="{Binding Name}" Detail="{Binding Status}"/>
        </DataTemplate>
    </ListView.ItemTemplate>
</ListView>
```

```c#
void Handle_Refreshing(object sender, EventArgs e) {
    // do something
    listview.IsRefreshing = false;
    //listView.EndRefreshing()
}
```

### Search Bar

```xaml
<StackLayout>
    <SearchBar Placeholder="Search..." TextChanged="Handle_TextChanged"/>
	<ListView x:Name="listView">
        <ListView.ItemTemplate>
            <DataTemplate>
                <TextCell Text="{Binding Name}" Detail="{Binding Status}"/>
            </DataTemplate>
        </ListView.ItemTemplate>
    </ListView>
</StackLayout>
```

or 

```xaml
<SearchBar Placeholder="Search..." SearchButtonPressed="Handle_SearchButtonPressed"/>
```

```c#
void Hanle_TextChanged() {
    var searchText = e.newTextValue;
    listView.ItemsSource = contacts.Where(c => c.Name.StartWith(searchText));
}
```

## Navigation

### Hierarchical Navigation

```xaml
<!-- MainPage -->
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="HelloWorld.MainPage"
             NavigationPage.HasNavigationBar="false"
             Title="Welcome">
	<StackLayout HorizontalOptions="Center" VerticalOptions="Center">
    	<Label Text="Welcome" HorizontalOptions="Center"/>
        <Button Text="Next" Clicked="Handle_Clicked"/>
    </StackLayout>
</ContentPage>
```

```xaml
<!-- IntroductionPage -->
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="HelloWorld.IntroductionPage"
             Title="Introduction">
	<StackLayout HorizontalOptions="Center" VerticalOptions="Center">
    	<Label Text="This is how the app works"/>
        <Button Text="Back" Clicked="Handle_Clicked"/>
    </StackLayout>
</ContentPage>
```

```c#
// MainPage
async void Handle_Clicked(object sender, EventArgs e) {
    await Navigation.PushAsync(new IntroductionPage());
}
```

```c#
// IntroductionPage
async void Handle_Clicked(object sender, System.EventArgs e) {
    await Navigation.PopAsync();
}

protected override bool OnBackButtonPressed() {
    return true;
}
```

```c#
// App
public App() {
    InitializeComponent();
    MainPage = new NavigationPage(new MainPage()) {
        BarBackgroundColor = Color.Gray;
        BarTextColor = Color.White;
    };
}
```

###   Modal Pages

```c#
// ModalPage
async void Handle_Clicked(object sender, EventArgs e) {
    await Navigation.PopModalAsync();
}

protected override bool OnBackButtonPressed() {
    return true;
}
```

```c#
async void Handle_Clicked(object sender, EventArgs e) {
    await Navigation.PushModalAsync(new ModalPage());
}
```

### Master Detail

```xaml
<!-- MainPage -->
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml">
	<ListView x:Name="listView" ItemSelected="Handle_ItemSelected">
    	<ListView.ItemTemplate>
        	<DataTemplate>
            	<TextCell Text="{Binding Name} Detail={Binding Status}"/>
            </DataTemplate>
        </ListView.ItemTemplate>
    </ListView>
</ContentPage>
```

```xaml
<!-- DetailPage -->
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             Title="{Binding Name}">
	<Label Text="{Binding Name}"/>
</ContentPage>
```

``` c#
// MainPage
async void Handle_ItemSelected(object sender, SelectedItemChangeEvent e) {
    if (e.SelectedItem == null)
        return;
    var contact = e.SelectedItem as Contact;
    await Navigation.PushAsync(new DetailPage(contact));
    listView.SelectedItem = null;
}
```

```c#
// DetailPage
public DetailPage(Contact contact) {
    if (contact == null)
        throw new ArgumentNullException();
    BindingContext = contact;
    InitializeComponent();
}
```

```c#
// App
public App() {
    InitializeComponent();
    MainPage = new NavigationPage(new MainPage());
}
```

### Master Detail Page  - 062

