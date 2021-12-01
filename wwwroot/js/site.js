// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var alertCount = 1;
var debug = true;
function ShowMessage(messageObject) {

	/*
	 <div class=" alert fixed-top col-md-12 invisible  alert-dismissible text-center"  style="z-index:9999;">
				<div id="divAlertText">hfjsdf lushflshdlfksd luhsdlfhlsdf</div>
				<a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
	</div>	 
	 */

	var alertElement = $("<div class=\" alert  col-md-12 invisible  alert-dismissible text-center \"  style=\"z-index: 9999; \"><a href=\"#\" class=\"close\" data-dismiss=\"alert\" aria-label=\"close\">&times;</a></div >");

	var textElement = $("<div> hfjsdf lushflshdlfksd luhsdlfhlsdf</div >");
	if (messageObject.type == "Error" && debug) {
		var innerMessageElement = textElement.clone();
		innerMessageElement.text(messageObject.innerMessage);
		alertElement.prepend(innerMessageElement);
	}
	textElement.text(messageObject.message);
	alertElement.prepend(textElement);
	var elementId = "alert_" + (alertCount);
	alertElement.attr("id", elementId);

	var area = $("#divMessageArea");

	var cls = "";
	if (messageObject.type == "Error") {
		cls = "alert-danger";
	}

	if (messageObject.type == "Success") {
		cls = "alert-success";
	}

	alertElement.addClass(cls);
	alertElement.toggleClass("invisible")

	area.append(alertElement);
	var timer = setTimeout(function () {
		alertElement.alert("close");
		clearTimeout(timer);
	}, 3000);








}


function require(script) {
	$.ajax({
		url: script,
		dataType: "script",
		async: false,           // <-- This is the key
		success: function () {
			// all good...
		},
		error: function () {
			throw new Error("Could not load script " + script);
		}
	});
}


function reverseDateString(str) {

	str = str.replace(/\//g, '-');

	// Step 1. Use the split() method to return a new array
	var splitString = str.split("-"); // var splitString = "hello".split("");
	// ["h", "e", "l", "l", "o"]

	// Step 2. Use the reverse() method to reverse the new created array
	var reverseArray = splitString.reverse(); // var reverseArray = ["h", "e", "l", "l", "o"].reverse();
	// ["o", "l", "l", "e", "h"]

	// Step 3. Use the join() method to join all elements of the array into a string
	var joinArray = reverseArray.join("-"); // var joinArray = ["o", "l", "l", "e", "h"].join("");
	// "olleh"

	//Step 4. Return the reversed string
	return joinArray; // "olleh"
}