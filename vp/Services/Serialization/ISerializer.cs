namespace vp.Services.Serialization
{
    /// <summary>
    /// Used for serializing and deserializing objects into strings
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Serializes the provided <see cref="object"/> into a <see cref="string"/>
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Serialized object</returns>
        string Serialize(object obj);

        /// <summary>
        /// Deserializes the provided <see cref="string"/> into the provided type <see cref="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns>Deserialized object</returns>
        T Deserialize<T>(string str);
    }
}
