# TouchGUI
An HTM-like touch-based mobile GUI framework in C# for Unity.

### Features:

* Optimized for touch-based input
* HTML-like syntax
* Stylesheets
* Event handlers
* Custom font support
* Wide variety of image scaling options
* Responsive positioning to fit any mobile screen 
* Hierarchical page layout for managing complex GUIs
* Buttons, input fields, tabs, modal dialogs and more

### Example Page Layout

```xml
<!-- Login page -->
<page id="pageLogin">
		
	<!-- Page header logo -->
	<img src="GUI_PNG/logo_cool-01" y="120" width="750" alignH="center" colour="rgb(204,204,204)"/>

	<!-- Page heading text -->
	<label style="lightGray" y="400" alignH="center" fontSize="60" textAlign="upperCenter">Please Login</label>
	
	<!-- ===========================================================================================================- -->
	
	<!-- Username field label -->
	<label style="loginLabel lightGray" x="50" y="600">Username</label>
	
	<!-- Username field -->
	<input style="loginInput" id="username" y="675">
		
		<!-- Field text and styling -->
		<text style="inputText"/>
		
		<!-- Background image -->
		<background style="inputBckg"/>
		
	</input>
	
	<!-- ===========================================================================================================- -->
	
	<!-- Password field label -->
	<label style="loginLabel lightGray" x="50" y="825">Password</label>
	
	<!-- Password field -->
	<input style="loginInput" id="password" type="password" y="900">
		
		<!-- Field text and styling -->
		<text style="inputText"/>
		
		<!-- Background image -->
		<background style="inputBckg"/>
		
		<!-- Reveal password icon -->
		<icon src="GUI_PNG/login_password-01-icon" colour="rgb(192,192,192)"/>		
		
	</input>
	
	<!-- ===========================================================================================================- -->
	
	<!-- Login button -->
	<button id="btnLogin" style="navButton" onClick="login_Click" x="0" y="700" alignH="center" alignV="bottom">
	
		<!-- Button text and styling -->
		<text style="navButton">Let's go!</text>
		
		<!-- Background image -->
		<background style="navButtonBckg"/>
	
	</button>
</page>
```

