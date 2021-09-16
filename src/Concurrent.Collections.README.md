# Collections.Concurrent

Using a ReaderWriterLockSlim to protect reads and writes on the ConcurrentHashSet. The popular solutions available on nuget don't support the core set operations required to make the hashset useful, so I had to hand-roll this.