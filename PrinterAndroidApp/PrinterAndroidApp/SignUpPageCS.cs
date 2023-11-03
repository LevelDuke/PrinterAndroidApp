using System;
using System.Linq;
using Xamarin.Forms;

namespace PrinterAndroidApp
{
    public class SignUpPageCS : ContentPage
    {
        Entry firstnameEntry, lastnameEntry, usernameEntry, passwordEntry, emailEntry;
        Label messageLabel;

        public SignUpPageCS()
        {
            messageLabel = new Label();

            firstnameEntry = new Entry
            {
                Placeholder = "firstname"
            };

            lastnameEntry = new Entry
            {
                Placeholder = "lastname"
            };

            usernameEntry = new Entry
            {
                Placeholder = "username"
            };

            passwordEntry = new Entry
            {
                IsPassword = true
            };

            emailEntry = new Entry();
            var signUpButton = new Button
            {
                Text = "Sign Up"
            };

            signUpButton.Clicked += OnSignUpButtonClicked;

            Title = "Sign Up";
            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                Children = {
                    new Label { Text = "First Name"},
                    firstnameEntry,
                    new Label { Text = "Last Name"},
                    lastnameEntry,
                    new Label { Text = "Username" },
                    usernameEntry,
                    new Label { Text = "Password" },
                    passwordEntry,
                    new Label { Text = "Email address" },
                    emailEntry,
                    signUpButton,
                    messageLabel
                }
            };
        }

        async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            var user = new User()
            {
                FirstName = firstnameEntry.Text,
                LastName = lastnameEntry.Text,
                Username = usernameEntry.Text,
                Password = passwordEntry.Text,
                Email = emailEntry.Text
            };

            // Sign up logic goes here

            var signUpSucceeded = AreDetailsValid(user);
            if (signUpSucceeded)
            {
                var rootPage = Navigation.NavigationStack.FirstOrDefault();
                if (rootPage != null)
                {
                    App.IsUserLoggedIn = true;
                    Navigation.InsertPageBefore(new MainPageCS(), Navigation.NavigationStack.First());
                    await Navigation.PopToRootAsync();
                }
            }
            else
            {
                messageLabel.Text = "Sign up failed";
            }
        }

        bool AreDetailsValid(User user)
        {
            return !string.IsNullOrWhiteSpace(user.FirstName)
                && !string.IsNullOrWhiteSpace(user.LastName)
                && !string.IsNullOrWhiteSpace(user.Username)
                && !string.IsNullOrWhiteSpace(user.Password)
                && !string.IsNullOrWhiteSpace(user.Email)
                && user.Email.Contains("@");
        }
    }
}

