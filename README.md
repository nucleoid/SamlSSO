# SamlSSO Setup

1. Setup an identity provider (idp) - Google was used as the identity provider and setup for that can be found on the [google support article](https://support.google.com/a/answer/6087519?hl=en)
2. Update the email found in SamlSSO/Services/UserService.cs to match your own
3. Update the IssuerURL and Certificate found in SamlSSO/Services/SamlIdentityService.cs
4. Compile code (in visual studio -> Build -> Rebuild Solution)
5. Start up your flavor of IIS with app.  No DB used, so only IIS is required.

Starting link:  http://localhost/SamlSSO/LoginSSO/MediaSuite  (this should redirect you to a google login, if google is the idp)

Important code (and starting point) found in SamlSSO/Controllers/HomeController.cs
