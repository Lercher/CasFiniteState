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
            context.CreateBookmark("TriggerEvent", (ctx, bm, value) => {
                Console.WriteLine("Event {0} triggered", EventId);
            });
        }
    }
}