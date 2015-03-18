$(document).ready(function()
{
    $("#firstpane div.activemenu").next("div.menu_body").slideToggle(300).siblings("div.menu_body").slideUp("slow");

	//slides the element with class "menu_body" when paragraph with class "menu_head" is clicked 
	$("#firstpane div.menu_head").click(function()
    {
		$(this).css({backgroundImage:"url(images/navigation_bg.gif)"}).next("div.menu_body").slideToggle(300).siblings("div.menu_body").slideUp("slow");
		
		$(this).css({backgroundImage:"url(images/navigation_hvr_bg.gif)"});
       	
	});
	//slides the element with class "menu_body" when mouse is over the paragraph
	$("#firstpane div.menu_head").mouseover(function()
    {
	     $(this).css({backgroundImage:"url(images/navigation_hvr_bg.gif)"});
	});
	$("#firstpane div.menu_head").mouseout(function()
    {
	     $(this).css({backgroundImage:"url(images/navigation_bg.gif)"});
	});
});// JavaScript Document