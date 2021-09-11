﻿using System.IO;

namespace Documents.IO
{
    public interface IDocumentSerializer<T>
        where T : class
    {
        Document<T> Deserialize(Stream stream);

        void Serialize(Document<T> document, Stream stream);
    }
}