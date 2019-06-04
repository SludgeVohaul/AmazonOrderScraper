using System;
using Infrastructure.Anonymizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InfrastructureTests.Anonymizer
{
    [TestClass]
    public class DocumentAnonymizerTests
    {
        [TestMethod]
        public void AnonymizeDocument()
        {
            const string document =
@"<!DOCTYPE html>
<html lang=""de-DE"">
<head>
<title>Blubber</title>
<script>
      alert(""Hallo Welt!"");
</script >
</head>
<body>
<h1>My page heading</h1>
<h2>This is example static page to get all the HTML tags and their<strong> childrens content </strong>and then
<span>translate</span >
that into<br>another language.
<span class=""a-declarative"" data-action=""a-popover"" data-a-popover=""bla"" data-p13n=""abcdefghi"">blubb</span>
<!--[if IE 6]>
<style type=""text/css""><!--
  # navbar.nav-sprite-v3 .nav-sprite {
     background-image: url(https://www.heise.de/images/image.gif);
  }
-->
<!--[endif]---->
<span class=""a-declarative"" data-action=""a-popover"" data-a-popover=""bla2"" data-p13n-tralala=""asdasd"">blubb2</span>
<img src=""http://bla.blubb.de/images/asd.jpg"" alt=""blubb"" title=""lalala"">
<span class=""a-declarative"" data-action=""a-modal"" data-a-modal=""blubber"">blubb</span>
</h2>
<a href=""http://www.heise.de/"" title=""Hier zu Heise"" data-p13n-blubb=""lala"">Heise</a>
<p>Something in footer</p>
</body>
</html>";

            string expected =
@"<html lang=""de-DE""><head>
<title>XXXXXXX</title>

</head>
<body>
<h1>XX XXXX XXXXXXX</h1>
<h2>XXXX XX XXXXXXX XXXXXX XXXX XX XXX XXX XXX XXXX XXXX XXX XXXXX<strong> XXXXXXXXX XXXXXXX </strong>XXX XXXX
<span>XXXXXXXXX</span>
XXXX XXXX<br>XXXXXXX XXXXXXXX.
<span class=""a-declarative"" data-action=""a-popover"" data-a-popover=""YYY"" data-p13n=""YYYYYYYYY"">XXXXX</span>


<span class=""a-declarative"" data-action=""a-popover"" data-a-popover=""YYYY"" data-p13n-tralala=""YYYYYY"">XXXXXX</span>
<img src=""data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAACXBIWXMAAA7EAAAOxAGVKw4bAAABM0lEQVRYhb3XXWrCQBhG4UOvvBCXISJSxG2XXJWuoEhXIqWUrOHthRNqY6Yz308bGJCYyXlEyExgdgjWgkFwnH/nPQQnwbNg07pwI3gVSPCZgRAcBWO557mKKPFzuXAao+CUFFcVUYmHEJX4PaIRdyEa8Z8IwVPjQhOiMz6NAcFWcMlAGOMXwXaaGEa44zc3cCPC8QiijHg8gMiLOxG58T9A2OOJCH88ARGPBxDd8YdOwxpYGcyrMid+yPaQqT6s/jseRyTE/YjEuB1hjI+67h1zEMb4h+BRcCifYwhj/F1wuJm7L+d8CEd8v/ADdvLsJ2Rfz3e//IX2TY2ubyy98ebj1Yh46d2WmxaWTsSbOt8NXKtaA/Edv5mwhAgtqRXEfbyCSFnPZ4h6fIYYMuIzxLAU/wLSuV+5SfP0BgAAAABJRU5ErkJggg=="" alt=""YYYYY"" title=""YYYYYY"">
<span class=""a-declarative"" data-action=""a-modal"" data-a-modal=""YYYYYYY"">XXXXX</span>
</h2>
<a href=""http://www.google.de"" title=""YYYY YY YYYYY"" data-p13n-blubb=""YYYY"">XXXXX</a>
<p>XXXXXXXXX XX XXXXXX</p>

</body></html>";

            var sut    = new DocumentAnonymizer();
            var actual = sut.Anonymize(document);

            // FIY: CRLF vs LF...
            Assert.AreEqual(expected.Replace(Environment.NewLine, ""), actual.Replace("\n", ""));
        }
    }
}
