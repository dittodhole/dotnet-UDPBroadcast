﻿using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

// ReSharper disable ExceptionNotThrown
// ReSharper disable ExceptionNotDocumented

namespace UDPBroadcast
{
  public class MessageSerializer : IMessageSerializer
  {
    /// <exception cref="Exception">A generic error has occurred during deserialization.</exception>
    /// <exception cref="ArgumentNullException"><paramref name="buffer" /> is <see langword="null" />.</exception>
    /// <exception cref="SerializationException">An error has occurred during deserialization.</exception>
    public IMessage Deserialize(byte[] buffer)
    {
      if (buffer == null)
      {
        throw new ArgumentNullException(nameof(buffer));
      }

      using (var memoryStream = new MemoryStream(buffer))
      {
        var binaryFormatter = new BinaryFormatter();
        var obj = binaryFormatter.Deserialize(memoryStream);
        var message = obj as IMessage;

        return message;
      }
    }

    /// <exception cref="Exception">A generic error has occurred during serialization.</exception>
    /// <exception cref="ArgumentNullException"><paramref name="message" /> is <see langword="null" />.</exception>
    /// <exception cref="SerializationException">An error has occurred during serialization.</exception>
    public byte[] Serialize(IMessage message)
    {
      if (message == null)
      {
        throw new ArgumentNullException(nameof(message));
      }

      byte[] buffer;

      var binaryFormatter = new BinaryFormatter();
      using (var memoryStream = new MemoryStream())
      {
        binaryFormatter.Serialize(memoryStream,
                                  message);

        buffer = memoryStream.ToArray();
      }

      return buffer;
    }
  }
}
