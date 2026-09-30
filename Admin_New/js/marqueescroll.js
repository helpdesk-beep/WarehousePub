///***********************************************
//* Cross browser Marquee II- © Dynamic Drive (www.dynamicdrive.com)
//* This notice MUST stay intact for legal use
//* Visit http://www.dynamicdrive.com/ for this script and 100s more.
//***********************************************/

//var delayb4scroll=0000 //Specify initial delay before marquee starts to scroll on page (2000=2 seconds)
//var marqueespeed=1 //Specify marquee scroll speed (larger is faster 1-10)
//var pauseit=1 //Pause marquee onMousever (0=no. 1=yes)?

//////NO NEED TO EDIT BELOW THIS LINE////////////

//var copyspeed=marqueespeed
//var pausespeed=(pauseit==0)? copyspeed: 0
//var actualheight=''
//var scroll=1;
//var stop=0;
//function scrollmarquee(){
//    if (stop==1)
//    {
//    cross_marquee.style.top=0;
//    return;
//    }
//    if (scroll==0)
//    {
//        return;
//    }
//if (parseInt(cross_marquee.style.top)>(actualheight*(-1)+8)) //if scroller hasn't reached the end of its height
//cross_marquee.style.top=parseInt(cross_marquee.style.top)-copyspeed+"px" //move scroller upwards
//else //else, reset to original position
//cross_marquee.style.top=parseInt(marqueeheight)+8+"px"
//}

//function initializemarquee(){
//cross_marquee=document.getElementById("vmarquee")
//if ( cross_marquee == null ) return false;
////cross_marquee.style.top=0
//marqueeheight=document.getElementById("marqueecontainer").offsetHeight;
//document.getElementById("marqueecontainer").style.overflow="hidden"; 

//document.getElementById("marqueecontainer").onmouseout = function(){ copyspeed=marqueespeed;}
//document.getElementById("marqueecontainer").onmouseover = function(){ copyspeed=pausespeed;}
// 
//actualheight=cross_marquee.offsetHeight //height of marquee content (much of which is hidden from view)
//if (window.opera || navigator.userAgent.indexOf("Netscape/7")!=-1){ //if Opera or Netscape 7x, add scrollbars to scroll and exit
//cross_marquee.style.height=marqueeheight+"px"
//cross_marquee.style.overflow="scroll"
//return
//}
//setTimeout('lefttime=setInterval("scrollmarquee()",100)', delayb4scroll)
//}

//if (window.addEventListener)
//window.addEventListener("load", initializemarquee, false)
//else if (window.attachEvent)
//window.attachEvent("onload", initializemarquee)
//else if (document.getElementById)
//window.onload=initializemarquee


//function switchMenu(playobj,pauseobj,stopobj) 
//{
//    var v_play = document.getElementById(playobj);
//    var v_pause = document.getElementById(pauseobj);
//    var v_stop = document.getElementById(stopobj);
//    
//    if (stop==1)
//    {
//            v_play.style.display='block';
//            v_pause.style.display='none';
//            document.getElementById("marqueecontainer").style.overflow="auto"; 
//            return;
//    }
//    if ( v_play.style.display != 'none' ) 
//    {
//        scroll=1;
//        stop=0;
//        v_play.style.display='none';
//        v_pause.style.display='block';
//        document.getElementById("marqueecontainer").style.overflow="hidden"; 
//        v_pause.focus();
//    }
//    else 
//    {
//        scroll=0;
//        stop=0;
//        v_play.style.display='block';
//        v_pause.style.display='none';
//        
//        v_play.focus();
//    }

//}

/***********************************************
* Cross browser Marquee II- © Dynamic Drive (www.dynamicdrive.com)
* This notice MUST stay intact for legal use
* Visit http://www.dynamicdrive.com/ for this script and 100s more.
***********************************************/

var delayb4scroll = 0000 //Specify initial delay before marquee starts to scroll on page (2000=2 seconds)
var marqueespeed = 2 //Specify marquee scroll speed (larger is faster 1-10)
var pauseit = 1 //Pause marquee onMousever (0=no. 1=yes)?

////NO NEED TO EDIT BELOW THIS LINE////////////

var copyspeed = marqueespeed
var pausespeed = (pauseit == 0) ? copyspeed : 0
var actualheight = ''
var actualheight1 = ''
var actualheight2 = ''
//var actualheight3 = ''
var scroll = 1;
var stop = 0;
function scrollmarquee() {
    if (stop == 1) {
        $.each(cross_marquee, function (e, res) {
            res.style.top = 0;
        });

        return;
    }
    if (scroll == 0) {
        return;
    }
    $.each(cross_marquee, function (e, res) {

        if (e == 0) {
            if (parseInt(res.style.top) > (actualheight * (-1) + 8)) //if scroller hasn't reached the end of its height
                res.style.top = parseInt(res.style.top) - copyspeed + "px" //move scroller upwards
            else //else, reset to original position
                res.style.top = parseInt(marqueeheight) + 8 + "px"
        }
        if (e == 1) {
            if (parseInt(res.style.top) > (actualheight1 * (-1) + 8)) //if scroller hasn't reached the end of its height
                res.style.top = parseInt(res.style.top) - copyspeed + "px" //move scroller upwards
            else //else, reset to original position
                res.style.top = parseInt(marqueeheight) + 8 + "px"
        }
        //if (e == 2) {
        //    if (parseInt(res.style.top) > (actualheight2 * (-1) + 8)) //if scroller hasn't reached the end of its height
        //        res.style.top = parseInt(res.style.top) - copyspeed + "px" //move scroller upwards
        //    else //else, reset to original position
        //        res.style.top = parseInt(marqueeheight) + 8 + "px"
        //}

        //if (e == 3) {
        //    if (parseInt(res.style.top) > (actualheight3 * (-1) + 8)) //if scroller hasn't reached the end of its height
        //        res.style.top = parseInt(res.style.top) - copyspeed + "px" //move scroller upwards
        //    else //else, reset to original position
        //        res.style.top = parseInt(marqueeheight) + 8 + "px"
        //}


    });
}

function initializemarquee() {
    if ($(".scroller").length > 0) {
        cross_marquee = $(".scroller")

        if (cross_marquee == null) return false;
        //cross_marquee.style.top=0

        //if (document.getElementById("marqueecontainer")) {
        //    marqueeheight = document.getElementById("marqueecontainer").offsetHeight;
        //    document.getElementById("marqueecontainer").style.overflow = "hidden";
        //   document.getElementById("marqueecontainer").onmouseout = function () { copyspeed = marqueespeed; }
        //   document.getElementById("marqueecontainer").onmouseover = function () { copyspeed = pausespeed; }
        //$('#marqueecontainer').mouseout(function () { copyspeed = marqueespeed; })
        //$('#marqueecontainer').mouseover(function () { copyspeed = pausespeed; })
        //}
        if (document.getElementById("marqueecontainerspeeches")) {
            marqueeheight = document.getElementById("marqueecontainerspeeches").offsetHeight;
            document.getElementById("marqueecontainerspeeches").style.overflow = "hidden";

            $('#marqueecontainerspeeches').mouseout(function () { copyspeed = marqueespeed; })
            $('#marqueecontainerspeeches').mouseover(function () { copyspeed = pausespeed; })
        }
        //if (document.getElementById("marqueecontainerPress")) {
        //    marqueeheight = document.getElementById("marqueecontainerPress").offsetHeight;
        //    document.getElementById("marqueecontainerPress").style.overflow = "hidden";

        //    $('#marqueecontainerPress').mouseout(function () { copyspeed = marqueespeed; })
        //    $('#marqueecontainerPress').mouseover(function () { copyspeed = pausespeed; })

        //}

        if (document.getElementById("marqueecontainermessage")) {
            marqueeheight = document.getElementById("marqueecontainermessage").offsetHeight;
            document.getElementById("marqueecontainermessage").style.overflow = "hidden";

            $('#marqueecontainermessage').mouseout(function () { copyspeed = marqueespeed; })
            $('#marqueecontainermessage').mouseover(function () { copyspeed = pausespeed; })

        }

        actualheight = cross_marquee[0].offsetHeight //height of marquee content (much of which is hidden from view)
        actualheight1 = cross_marquee[1].offsetHeight //height of marquee content (much of which is hidden from view)
        //actualheight2 = cross_marquee[2].offsetHeight //height of marquee content (much of which is hidden from view)
        //actualheight2 = cross_marquee[3].offsetHeight //height of marquee content (much of which is hidden from view)



        if (window.opera || navigator.userAgent.indexOf("Netscape/7") != -1) { //if Opera or Netscape 7x, add scrollbars to scroll and exit
            $.each(cross_marquee, function (e, res) {
                res.style.height = marqueeheight + "px"
                res.style.overflow = "scroll"
            });

            return
        }
        setTimeout('lefttime=setInterval("scrollmarquee()",100)', delayb4scroll)
    }
}

if (window.addEventListener)
    window.addEventListener("load", initializemarquee, false)
else if (window.attachEvent)
    window.attachEvent("onload", initializemarquee)
else if (document.getElementById)
    window.onload = initializemarquee


function switchMenu(playobj, pauseobj, stopobj) {
    var v_play = document.getElementById(playobj);
    var v_pause = document.getElementById(pauseobj);
    var v_stop = document.getElementById(stopobj);

    if (stop == 1) {
        v_play.style.display = 'block';
        v_pause.style.display = 'none';
        //document.getElementById("marqueecontainer").style.overflow = "auto";
        document.getElementById("marqueecontainerspeeches").style.overflow = "auto";
        //document.getElementById("marqueecontainerPress").style.overflow = "auto";
        document.getElementById("marqueecontainermessage").style.overflow = "auto";

        return;
    }
    if (v_play.style.display != 'none') {
        scroll = 1;
        stop = 0;
        v_play.style.display = 'none';
        v_pause.style.display = 'block';
        //document.getElementById("marqueecontainer").style.overflow = "hidden";
        document.getElementById("marqueecontainerspeeches").style.overflow = "hidden";
        //document.getElementById("marqueecontainerPress").style.overflow = "hidden";
        document.getElementById("marqueecontainermessage").style.overflow = "hidden";
        v_pause.focus();
    }
    else {
        scroll = 0;
        stop = 0;
        v_play.style.display = 'block';
        v_pause.style.display = 'none';

        v_play.focus();
    }

}


