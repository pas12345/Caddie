using System;
using umbraco.interfaces;


namespace Caddie.Controllers
{
  //  public class ActionMatchSettlement : IAction
  //  {
  //      /// <summary>
  //      /// Private field for the singleton instance
  //      /// </summary>
  //      private static readonly ActionMatchSettlement instance = new ActionMatchSettlement();

  //      /// <summary>
  //      /// Gets a singleton instance of this action
  //      /// </summary>
  //      public static ActionMatchSettlement Instance
  //      {
  //          get { return instance; }
  //      }

  //      #region IAction Members

  //      /// <summary>
  //      /// Gets a string alias used to identify this action
  //      /// </summary>
  //      public string Alias
  //      {
  //          get { return "matchSettlement"; }
  //      }
  //      /// <summary>
  //      /// Gets a value indicating whether this action can be configured for use only by specific members
  //      /// </summary>
  //      public bool CanBePermissionAssigned
  //      {
  //          get { return false; }
  //      }
  //      /// <summary>
  //      /// Gets an icon to be used for the right click action 
  //      /// </summary>
  //      public string Icon
  //      {
  //          get { return "enter"; }
  //      }

 
		///// <summary>
		///// Gets a javascript string to execute when this action is fired
		///// </summary>
  //      public string JsFunctionName
  //      {
  //          get { return "matchSettlement()"; }
  //      }
  //      /// <summary>
  //      /// Gets a string for the javascript source
  //      /// </summary>
  //      public string JsSource
  //      {
  //          get
  //          {
  //              return @"function matchSettlement() {
  //                  $.ajax({
  //                      type: \""POST\"",
  //                      url: \""~/backoffice/Caddie/ResultApi/SettleMatch\"",
  //                      contentType: \""application/json;charset=utf-8\"",
  //                      data: \""{ 'matchId' : '456' },
  //                      dataType: \""json\"",
  //                      success: function(data) {
  //                          alert('ok');
  //                      },
  //                      error: function(data) { 
  //                          alert('ok');
  //                      }
  //                  });
  //              }
  //              ";
  //          }
  //      }
  //      public char Letter
  //      {
  //          get { return '*'; }
  //      }

  //      /// <summary>
  //      /// Gets a value indicating whether the Umbraco notification area is used ?
  //      /// </summary>
  //      public bool ShowInNotifier
  //      {
  //          get { return false; }
  //      }

  //      #endregion
  //  }
}
