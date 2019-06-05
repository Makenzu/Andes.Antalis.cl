function muestraMensaje(tipo, message, timeout, add)
{            
	if (typeof _statusbar == "undefined")    
	{       
		// ** Create a new statusbar instance as a global object        
		_statusbar = $("<div id='_statusbar' class='statusbar-" + tipo + "'></div>")                  
		.appendTo(document.body)                                       
		.show();
	}     
	if (add)                    
		// *** add before the first item            
		_statusbar.prepend( "<div style='margin-bottom: 2px;' >" + message + "</div>")[0].focus();    
	 else            
		_statusbar.text(message)     
	 _statusbar.show();             
	 
	 if (timeout)    
	 {        
		_statusbar.addClass("statusbarhighlight-" + tipo);        
		setTimeout( function() { 
			_statusbar.removeClass("statusbarhighlight-" + tipo); 
			_statusbar.fadeOut(500);
		},timeout);    
	}                
}


 