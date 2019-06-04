using System.Linq;
using System.Text.RegularExpressions;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using UseCases;

namespace Infrastructure.Anonymizer
{
    public class DocumentAnonymizer : IDocumentAnonymizer
    {
        // FYI: Do not use "^" and "$" the url might be in the middle of an attribute value.
        private static readonly Regex ImageUrl = new Regex("http.*(jpg|png|gif)");

        private static readonly Regex[] PersonalDataAttributePatterns =
        {
            new Regex(@"^data-a-popover$"),
            new Regex(@"^data-a-modal$"),
            new Regex(@"^alt$"),
            new Regex(@"^title$"),
            new Regex(@"^data-p13n.*$"),
            new Regex(@"^data-yo.*$")
        };

        private static readonly string ImagePng32x32Base64 =
            @"iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAACXBIWXMAAA7EAAAOxAGVKw4bAAABM0lEQVRYhb3XXWrCQBhG4UOvvB"
            +"CXISJSxG2XXJWuoEhXIqWUrOHthRNqY6Yz308bGJCYyXlEyExgdgjWgkFwnH/nPQQnwbNg07pwI3gVSPCZgRAcBWO557mKKPFzuXAa"
            +"o+CUFFcVUYmHEJX4PaIRdyEa8Z8IwVPjQhOiMz6NAcFWcMlAGOMXwXaaGEa44zc3cCPC8QiijHg8gMiLOxG58T9A2OOJCH88ARGPBx"
            +"Dd8YdOwxpYGcyrMid+yPaQqT6s/jseRyTE/YjEuB1hjI+67h1zEMb4h+BRcCifYwhj/F1wuJm7L+d8CEd8v/ADdvLsJ2Rfz3e//IX2"
            +"TY2ubyy98ebj1Yh46d2WmxaWTsSbOt8NXKtaA/Edv5mwhAgtqRXEfbyCSFnPZ4h6fIYYMuIzxLAU/wLSuV+5SfP0BgAAAABJRU5Erk"
            +"Jggg==";

        public string Anonymize(string document)
        {
            // TODO - unit test

            var parser = new HtmlParser();
            var dom    = parser.ParseDocument(document);

            // FYI: Create an array, otherwise Remove() on the script elements will alter the current iteration of the DOM.
            foreach (var node in dom.GetNodes<INode>().ToArray())
            {
                if (node is IHtmlScriptElement script)
                {
                    script.Remove();
                    continue;
                }

                if (node is IComment comment)
                {
                    comment.Remove();
                    continue;
                }

                if (node is IHtmlAnchorElement anchor) anchor.Href = "http://www.google.de";
                if (node is IText text) text.TextContent           = text.TextContent.AnonymizeWith("X");

                if (node is IHtmlElement element)
                {
                    foreach (var matchingAttribute in element.Attributes.Where(att => PersonalDataAttributePatterns.Any(re => re.IsMatch(att.Name))))
                    {
                        matchingAttribute.Value = matchingAttribute.Value.AnonymizeWith("Y");
                    }

                    // Replace all image-URLs
                    // FYI: Replacing the Href of an IAnchorElement is not enough - there's code in various attributes.
                    foreach (var matchingAttribute in element.Attributes.Where(x => ImageUrl.IsMatch(x.Value)))
                    {
                        matchingAttribute.Value = $"data:image/png;base64,{ImagePng32x32Base64}";
                    }
                }
            }

            return dom.DocumentElement.OuterHtml;
        }
    }

    internal static class StringExtensions
    {
        public static string AnonymizeWith(this string text, string anonymizedValue)
        {
            return Regex.Replace(text, @"\w", anonymizedValue);
        }
    }
}