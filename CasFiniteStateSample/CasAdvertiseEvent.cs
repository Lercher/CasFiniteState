using System;
using System.Activities;

namespace CasFiniteStateSample
{
    public class CasAdvertiseEvent : NativeActivity
    {
        public string EventId { get; set; }
        protected override void CacheMetadata(NativeActivityMetadata metadata)
        {
            if (string.IsNullOrWhiteSpace(EventId))
                metadata.AddValidationError("Please provide an Event Id");
            base.CacheMetadata(metadata);
        }

        protected override bool CanInduceIdle => true;
        protected override void Execute(NativeActivityContext context)
        {
            Console.WriteLine("Waiting for Event {0} to be triggered", EventId);
            var bmn = string.Format("Trigger-{0}", EventId);
            context.CreateBookmark(bmn, OnResumeBookmark);
        }

        public void OnResumeBookmark(NativeActivityContext context, Bookmark bm, object value)
        {
            Console.WriteLine("Event {0} triggered", EventId);
        }
    }
}