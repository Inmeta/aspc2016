'use strict';

ExecuteOrDelayUntilScriptLoaded(initializePage, "sp.js");

function initializePage() {
    var context = SP.ClientContext.get_current();
    var web = context.get_web()
    var user = web.get_currentUser();
    //var oList = web.get_lists().getByTitle('MonitorLiveData');

    var SPAppWebUrl; // the app web URL
    var SPHostUrl; // the host web URL

    // This code runs when the DOM is ready and creates a context object which is needed to use the SharePoint object model
    $(document).ready(function () {
        SPAppWebUrl = decodeURIComponent(getQueryStringParameter("SPAppWebUrl"));
        SPHostUrl = decodeURIComponent(getQueryStringParameter("SPHostUrl"));

        // resources are in URLs in the form:
        // web_url/_layouts/15/resource
        var scriptbase = SPHostUrl + "/_layouts/15/";

        // Load the js files and continue to the successHandler
        $.getScript(scriptbase + "SP.RequestExecutor.js", execCrossDomainRequest);

        getUserName();
        //getLiveData();
    });

    function execCrossDomainRequest() {
        // executor: The RequestExecutor object
        // Initialize the RequestExecutor with the add-in web URL.
        alert(SPAppWebUrl);
        var executor = new SP.RequestExecutor(SPAppWebUrl);

        // Issue the call against the add-in web.
        // To get the title using REST we can hit the endpoint:
        //      appweburl/_api/web/lists/getbytitle('listname')/items
        // The response formats the data in the JSON format.
        // The functions successHandler and errorHandler attend the
        //      sucess and error events respectively.
        executor.executeAsync(
            {
                url:
                    SPHostUrl +
                    "/_api/web/lists/getbytitle('MonitorLiveData')/items",
                method: "GET",
                headers: { "Accept": "application/json; odata=verbose" },
                success: successHandler,
                error: errorHandler
            }
        );
    }

    // Function to handle the success event.
    // Prints the data to the page.
    function successHandler(data) {
        var jsonObject = JSON.parse(data.body);
        var announcementsHTML = "";

        alert('I got data');

        //var results = jsonObject.d.results;
        //for (var i = 0; i < results.length; i++) {
        //    announcementsHTML = announcementsHTML +
        //        "<p><h1>" + results[i].Title +
        //        "</h1>" + results[i].Body +
        //        "</p><hr>";
        //}

        //document.getElementById("renderAnnouncements").innerHTML =
        //    announcementsHTML;
    }

    // Function to handle the error event.
    // Prints the error message to the page.
    function errorHandler(data, errorCode, errorMessage) {
        document.getElementById("renderAnnouncements").innerText =
            "Could not complete cross-domain call: " + errorMessage;
    }

    // This function prepares, loads, and then executes a SharePoint query to get the current users information
    function getUserName() {
        context.load(user);
        context.executeQueryAsync(onGetUserNameSuccess, onGetUserNameFail);
    }

    function getLiveData() {



        var url = SPAppWebUrl + "/_api/SP.AppContextSite(@target)" + "/web/lists/getbytitle('MonitorLiveData')/items?" +
        "@target='" + SPHostUrl + "'";

        var executor = new SP.RequestExecutor(SPAppWebUrl);
        executor.executeAsync({
            url: url,
            method: "GET",
            headers: { "Accept": "application/json;odata=verbose" }, // return data format
            success: function (data) {
                // parse the returned data
                var body = JSON.parse(data.body);
                $("#message").html("Found " + body.d.results.length + " items.");
            },
            error: function (data) {
                $("#message").html("Failed to read items.");
            }
        });


        //alert('start');
        //var camlQuery = new SP.CamlQuery();
        //alert('start 1');
        //camlQuery.set_viewXml('<View><Query><Where><Geq><FieldRef Name=\'DataType\'/>' +
        //    '<Value Type=\'Text\'>HeartRate</Value></Geq></Where></Query><RowLimit>1</RowLimit></View>');
        //alert('start 2');
        //this.collListItem = oList.getItems(camlQuery);
        //alert('end');
        //clientContext.load(collListItem);
        //alert('context 1');

        //clientContext.executeQueryAsync(Function.createDelegate(this, this.onQuerySucceeded), Function.createDelegate(this, this.onQueryFailed));
        //alert('query');
    }

    // This function is executed if the above call is successful
    // It replaces the contents of the 'message' element with the user name
    function onGetUserNameSuccess() {
        $('#message').text('Hello ' + user.get_title());
    }

    // This function is executed if the above call fails
    function onGetUserNameFail(sender, args) {
        alert('Failed to get user name. Error:' + args.get_message());
    }



    // Function to retrieve a query string value.
    // For production purposes you may want to use
    //  a library to handle the query string.
    function getQueryStringParameter(paramToRetrieve) {
        var params =
            document.URL.split("?")[1].split("&");
        var strParams = "";
        for (var i = 0; i < params.length; i = i + 1) {
            var singleParam = params[i].split("=");
            if (singleParam[0] == paramToRetrieve)
                return singleParam[1];
        }
    }
    









    function onQuerySucceeded(sender, args) {
        alert('got it!');
        var listItemInfo = '';

        var listItemEnumerator = collListItem.getEnumerator();

        while (listItemEnumerator.moveNext()) {
            var oListItem = listItemEnumerator.get_current();
            listItemInfo += '\nID: ' + oListItem.get_id() +
                '\nTitle: ' + oListItem.get_item('DataType') +
                '\nBody: ' + oListItem.get_item('DataValue');
        }

        //alert(listItemInfo.toString());
        $('#message').text('Hello ' + user.get_title());
    }

    function onQueryFailed(sender, args) {

        alert('Request failed. ' + args.get_message() + '\n' + args.get_stackTrace());
    }



}
