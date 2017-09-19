using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using uFramework.Common.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace uFramework.MVC.Extensions
{
    public static class TinyMceExtensions
    {
        public static MvcHtmlString TinyMce
            (this HtmlHelper htmlHelper, string name, string value)
        {
            return TinyMce(htmlHelper, name, value, TinyMcePresets.Default);
        }

        public static MvcHtmlString TinyMce
            (this HtmlHelper htmlHelper, string name, string value, object options)
        {
            return TinyMce(htmlHelper, name, value, HtmlHelper.AnonymousObjectToHtmlAttributes(options));
        }

        public static MvcHtmlString TinyMce
            (this HtmlHelper htmlHelper, string name, string value, IDictionary<string, object> options)
        {
            var textArea =
                htmlHelper.TextArea(name, value, new { @id = name });

            var sync =
                new { setup = new JRaw("function (editor) { editor.on('change', function () { editor.save(); }) }") };

            var merged =
                JObject.FromObject(options);

            merged.Merge(JObject.FromObject(sync));

            var settings =
                (options == null) ? string.Empty : JsonConvert.SerializeObject(merged);

            var script = new TagBuilder("script");
            script.Attributes["type"] = "text/javascript";

            script.InnerHtml = @"
                    $(function() {
                        $('#" + name + "').tinymce(" + settings + @");
                    });";

            return MvcHtmlString.Create(textArea + script.ToString());
        }

        public static MvcHtmlString TinyMceFor<TModel, TProperty>
            (this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
            where TModel : class
        {
            return TinyMceFor(htmlHelper, expression, TinyMcePresets.Default);
        }

        public static MvcHtmlString TinyMceFor<TModel, TProperty>
            (this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object options)
            where TModel : class
        {
            return TinyMceFor(htmlHelper, expression, HtmlHelper.AnonymousObjectToHtmlAttributes(options));
        }

        public static MvcHtmlString TinyMceFor<TModel, TProperty>
            (this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> options)
            where TModel : class
        {
            var value =
                (htmlHelper.ViewData.Model == null) ? string.Empty : expression.GetValueFrom(htmlHelper.ViewData.Model) as string;

            return TinyMce(htmlHelper, HtmlHelper.GenerateIdFromName(expression.GetNameFor()), value, options);
        }
    }

    public static class TinyMcePresets
    {
        public static IDictionary<string, object> Default
        {
            get
            {
                return new RouteValueDictionary(new
                {
                    language = "en",
                    width = "550px",
                    theme = "modern",
                    plugins = new string[]
                        {
                            "advlist autolink lists link image charmap print preview anchor",
                            "searchreplace visualblocks code fullscreen",
                            "insertdatetime media table contextmenu paste"
                        },
                    dialog_type = "modal",
                    paste_auto_cleanup_on_paste = false,
                    paste_strip_class_attributes = "all",
                    paste_remove_spans = true,
                    force_p_newlines = false,
                    forced_root_block = false,
                    paste_retain_style_properties = "",
                    table_cell_limit = 100,
                    table_row_limit = 5,
                    table_col_limit = 5,
                    theme_advanced_buttons1 = "newdocument,|,bold,italic,underline,|,justifyleft,justifycenter,justifyright,fontselect,fontsizeselect,formatselect",
                    theme_advanced_buttons2 = "cut,copy,paste,pasteword,selectall,|,bullist,numlist,|,outdent,indent,|,undo,redo,|,link,unlink,anchor,image,|,code,|,forecolor,backcolor",
                    theme_advanced_buttons3 = "advhr,,removeformat,|,sub,sup,|,tablecontrols",
                    theme_advanced_toolbar_location = "top",
                    theme_advanced_toolbar_align = "left",
                    theme_advanced_statusbar_location = "bottom",
                    theme_advanced_resizing = true
                });
            }
        }

        public static IDictionary<string, object> Lite
        {
            get
            {
                return new RouteValueDictionary(new
                {
                    menubar = false,
                    language = "en",
                    mode = "specific_textareas",
                    editor_selector = "mceEditor",
                    width = "550px",
                    theme = "modern",
                    plugins = new string[]
                        {
                            "code contextmenu paste"
                        },
                    force_p_newlines = false,
                    forced_root_block = false,
                    toolbar = "undo redo | cut,copy,paste | bold italic underline | code",
                    theme_advanced_toolbar_location = "top",
                    theme_advanced_toolbar_align = "left",
                    theme_advanced_statusbar_location = "bottom",
                    theme_advanced_resizing = true
                });
            }
        }

        public static IDictionary<string, object> Basic
        {
            get
            {
                return new RouteValueDictionary(new
                {
                    menubar = false,
                    language = "en",
                    mode = "specific_textareas",
                    editor_selector = "mceEditor",
                    width = "550px",
                    theme = "modern",
                    plugins = new string[]
                        {
                            "advlist autolink lists link image charmap print preview anchor",
                            "searchreplace visualblocks code fullscreen",
                            "insertdatetime media table contextmenu paste",
                            "textcolor colorpicker "
                        },
                    force_p_newlines = false,
                    forced_root_block = false,
                    toolbar = "insertfile undo redo | styleselect | bold italic forecolor backcolor | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent"
                });
            }
        }

        public static IDictionary<string, object> ReadOnly
        {
            get
            {
                return new RouteValueDictionary(new
                {
                    menubar = false,
                    language = "en",
                    mode = "specific_textareas",
                    editor_selector = "mceEditor",
                    width = "550px",
                    theme = "modern",
                    @readonly = 1,
                    plugins = new string[]
                        {
                            "code contextmenu paste"
                        },
                    force_p_newlines = false,
                    forced_root_block = false,
                    toolbar = "undo redo | cut,copy,paste | bold italic underline | code",
                    theme_advanced_toolbar_location = "top",
                    theme_advanced_toolbar_align = "left",
                    theme_advanced_statusbar_location = "bottom",
                    theme_advanced_resizing = true
                });
            }
        }
    }

}
