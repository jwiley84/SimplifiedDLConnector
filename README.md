# Simplified Directline Connector


## How this works

The bot uses .NET core priciples, using the default.htm file set in Startup.cs. Upon initialization, default.htm calls the script with the embedded async function. This function uses the POST method on the /directline/token endpoint. 

The bot's TokenController waits for a request to this endpoint, and subsequently runs it's own function: TokenRequest(). Using the secret saved in appsettings.json (and configured for use by Startup.cs), TokenRequest() makes an http request to "https://directline.botframework.com/v3/directline/tokens/generate" with the secret in the header, and a new userID (so each user can have individual IDs) in the body. 

This request will return a successful response (HTTP 200) or unsuccessful (HTTP 500). If the result is 200, the body of the response is read for the token, which is returned to complete the function. This return travels all the way up to the initial call in the default.htm, which uses the returned token to render the webchat window.
