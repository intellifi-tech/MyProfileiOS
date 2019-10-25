using System;

using UIKit;
using CoreGraphics;
using Foundation;
using FFImageLoading;
using FFImageLoading.Work;
using static MyProfileiOS.ChatDetayBase;

namespace MyProfileiOS.Mesajlar.Cells
{
	public abstract class BubbleCell : UITableViewCell
	{
		public UIImageView BubbleImageView { get; private set; }
		public UILabel MessageLabel { get; private set; }
		public UIImage BubbleImage { get; set; }
		public UIImage BubbleHighlightedImage { get; set; }

        Message msg;

		public Message Message {
			get {
				return msg;
			}
			set {
				msg = value;
				BubbleImageView.Image = BubbleImage;
				BubbleImageView.HighlightedImage = BubbleHighlightedImage;
				MessageLabel.Text = msg.message;
				MessageLabel.UserInteractionEnabled = true;
				BubbleImageView.UserInteractionEnabled = false;
                //Hediye.UserInteractionEnabled = false;
                //Hediye.ContentMode = UIViewContentMode.ScaleAspectFit;

                //var Boll = msg.message.Split('#');
                //if (Boll.Length > 1)//Resim
                //    SetGiftImage(msg.message);
            }
		}

		public BubbleCell (IntPtr handle)
			: base (handle)
		{
			Initialize ();
		}

		public BubbleCell ()
		{
			Initialize ();
		}

		[Export ("initWithStyle:reuseIdentifier:")]
		public BubbleCell (UITableViewCellStyle style, string reuseIdentifier)
			: base (style, reuseIdentifier)
		{
			Initialize ();
		}

		void Initialize ()
		{
			BubbleImageView = new UIImageView {
				TranslatesAutoresizingMaskIntoConstraints = false
			};
			MessageLabel = new UILabel {
				TranslatesAutoresizingMaskIntoConstraints = false,
				Lines = 0,
				PreferredMaxLayoutWidth = 220f
			};

           // Hediye = new UIImageView()
           // {
           //     //TranslatesAutoresizingMaskIntoConstraints = false,
           // };

           // Hediye.Frame = new CGRect(0, 0, 200, 200);
           //// Hediye.Image = UIImage.FromBundle("Images/gow.png");

            ContentView.AddSubviews(BubbleImageView,MessageLabel);
            ContentView.BackgroundColor = UIColor.Clear;
        }

        public override void SetSelected (bool selected, bool animated)
		{
			base.SetSelected (selected, animated);
			BubbleImageView.Highlighted = selected;
		}

		protected static UIImage CreateColoredImage (UIColor color, UIImage mask)
		{
			var rect = new CGRect (CGPoint.Empty, mask.Size);
			UIGraphics.BeginImageContextWithOptions (mask.Size, false, mask.CurrentScale);
			CGContext context = UIGraphics.GetCurrentContext ();
			mask.DrawAsPatternInRect (rect);
			context.SetFillColor (color.CGColor);
			context.SetBlendMode (CGBlendMode.SourceAtop);
			context.FillRect (rect);
			UIImage result = UIGraphics.GetImageFromCurrentImageContext ();
			UIGraphics.EndImageContext ();
			return result;
		}

		protected static UIImage CreateBubbleWithBorder (UIImage bubbleImg, UIColor bubbleColor)
		{
			bubbleImg = CreateColoredImage (bubbleColor, bubbleImg);
			CGSize size = bubbleImg.Size;

			UIGraphics.BeginImageContextWithOptions (size, false, 0);
			var rect = new CGRect (CGPoint.Empty, size);
			bubbleImg.Draw (rect);

			var result = UIGraphics.GetImageFromCurrentImageContext ();
			UIGraphics.EndImageContext ();

			return result;
		}
	}
}