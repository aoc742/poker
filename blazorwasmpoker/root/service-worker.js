// In development, always fetch from the network and do not enable offline support.
// This is because caching would make development more difficult (changes would not
// be reflected on the first load after each change).

// This JS file is called before loading the app. This file defines how offline support works (what functionality is supported offline, what data gets cached, etc.)
self.addEventListener('fetch', () => { });
