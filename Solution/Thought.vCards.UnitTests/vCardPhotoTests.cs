
using System;
using System.Drawing;
using System.IO;
using NUnit.Framework;
using Thought.vCards;

namespace Tests
{
    [TestFixture]
    public class vCardPhotoTests
    {
        static vCardPhotoTests()
        {
            // Accept TLS 1.0, 1.1, 1.2
            System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)(192 | 768 | 3072);
        }
        /// <summary>
        ///     The URL of an image that is under control of the author
        ///     and is sufficiently small to allow quick download.  If you
        ///     use the vCard library internally w/ extensive unit tests,
        ///     please be considerate and change to an image on your
        ///     local network.  This will save bandwidth costs for the author.
        /// </summary>
        private const string TestPhotoUrl =
            "https://github.com/eaksi/publicdomainicons/raw/master/folder_open.png";

        /// <summary>
        ///     The height of the test image in pixels.
        /// </summary>
        private const int TestPhotoHeight = 32;

        /// <summary>
        ///     The size (in bytes) of the test image.
        /// </summary>
        private const int TestPhotoSize = 796;

        /// <summary>
        ///     The width of the test photo in pixels.
        /// </summary>
        private const int TestPhotoWidth = 32;

        // 

        #region [ Constructor_String ]

        [Test]
        public void Constructor_String()
        {

            // If a filename (string) is passed to the constructor, then
            // the scheme of the URI should be set as file.

            vCardPhoto photo = new vCardPhoto("c:\\fake-picture.gif");

            Assert.IsTrue(
                photo.Url.IsFile);

        }

        #endregion

        #region [ Constructor_String_Empty ]

        [Test]
        public void Constructor_String_Empty()
        {
            Assert.That(() => {
                vCardPhoto photo = new vCardPhoto(string.Empty);
            }, Throws.TypeOf<ArgumentNullException>());
        }

        #endregion

        #region [ Constructor_String_Null ]

        [Test]
        public void Constructor_String_Null()
        {
            Assert.That(() => {
                vCardPhoto photo = new vCardPhoto((string)null);
            }, Throws.TypeOf<ArgumentNullException>());
        }

        #endregion

        #region [ Constructor_Uri_Null ]

        [Test]
        public void Constructor_Uri_Null()
        {
            Assert.That(() => {
                new vCardPhoto((Uri)null);
            }, Throws.TypeOf<ArgumentNullException>());
        }

        #endregion

        #region [ Fetch_Good ]

        [Test]
        public void Fetch_Good()
        {

            // You may wish to ignore this test if you run
            // extensive unit tests and your Internet connection
            // is slow.

            vCardPhoto photo = new vCardPhoto(TestPhotoUrl);

            Assert.IsFalse(
                photo.IsLoaded,
                "The photo has not been loaded yet.");

            photo.Fetch();

            Assert.IsTrue(
                photo.IsLoaded,
                "The photo should have been loaded.");

            // Get the bytes of the image.

            byte[] data = photo.GetBytes();

            Assert.AreEqual(
                TestPhotoSize,
                data.Length,
                "The length of the photo is unexpected.");

            using (Bitmap bitmap = photo.GetBitmap())
            {

                Assert.AreEqual(
                    TestPhotoHeight,
                    bitmap.Size.Height,
                    "The photo height is unexpected.");

                Assert.AreEqual(
                    TestPhotoWidth,
                    bitmap.Size.Width,
                    "The photo width is unexpected.");

            }


        }

        #endregion

    }
}
