#if ENABLE_PLAYFABENTITY_API
using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.DataModels
{
    [Serializable]
    public class AbortFileUploadsRequest : PlayFabRequestCommon
    {
        /// <summary>
        /// The entity to perform this action on.
        /// </summary>
        public EntityKey Entity;
        /// <summary>
        /// Names of the files to have their pending uploads aborted.
        /// </summary>
        public List<string> FileNames;
        /// <summary>
        /// The expected version of the profile, if set and doesn't match the current version of the profile the operation will not
        /// be performed.
        /// </summary>
        public int? ProfileVersion;
    }

    [Serializable]
    public class AbortFileUploadsResponse : PlayFabResultCommon
    {
        /// <summary>
        /// The entity id and type.
        /// </summary>
        public EntityKey Entity;
        /// <summary>
        /// The current version of the profile, can be used for concurrency control during updates.
        /// </summary>
        public int ProfileVersion;
    }

    [Serializable]
    public class DeleteFilesRequest : PlayFabRequestCommon
    {
        /// <summary>
        /// The entity to perform this action on.
        /// </summary>
        public EntityKey Entity;
        /// <summary>
        /// Names of the files to be deleted.
        /// </summary>
        public List<string> FileNames;
        /// <summary>
        /// The expected version of the profile, if set and doesn't match the current version of the profile the operation will not
        /// be performed.
        /// </summary>
        public int? ProfileVersion;
    }

    [Serializable]
    public class DeleteFilesResponse : PlayFabResultCommon
    {
        /// <summary>
        /// The entity id and type.
        /// </summary>
        public EntityKey Entity;
        /// <summary>
        /// The current version of the profile, can be used for concurrency control during updates.
        /// </summary>
        public int ProfileVersion;
    }

    /// <summary>
    /// Entity identifier class that contains both the ID and type.
    /// </summary>
    [Serializable]
    public class EntityKey
    {
        /// <summary>
        /// Entity profile ID.
        /// </summary>
        public string Id;
        /// <summary>
        /// Entity type. Optional to be used but one of EntityType or EntityTypeString must be set.
        /// </summary>
        public EntityTypes? Type;
        /// <summary>
        /// Entity type. Optional to be used but one of EntityType or EntityTypeString must be set.
        /// </summary>
        public string TypeString;
    }

    public enum EntityTypes
    {
        title,
        master_player_account,
        title_player_account,
        character,
        group,
        service
    }

    [Serializable]
    public class FinalizeFileUploadsRequest : PlayFabRequestCommon
    {
        /// <summary>
        /// The entity to perform this action on.
        /// </summary>
        public EntityKey Entity;
        /// <summary>
        /// Names of the files to be finalized. Restricted to a-Z, 0-9, '(', ')', '_', '-' and '.'
        /// </summary>
        public List<string> FileNames;
    }

    [Serializable]
    public class FinalizeFileUploadsResponse : PlayFabResultCommon
    {
        /// <summary>
        /// The entity id and type.
        /// </summary>
        public EntityKey Entity;
        /// <summary>
        /// Collection of metadata for the entity's files
        /// </summary>
        public Dictionary<string,GetFileMetadata> Metadata;
        /// <summary>
        /// The current version of the profile, can be used for concurrency control during updates.
        /// </summary>
        public int ProfileVersion;
    }

    [Serializable]
    public class GetFileMetadata
    {
        /// <summary>
        /// Checksum value for the file
        /// </summary>
        public string Checksum;
        /// <summary>
        /// Download URL where the file can be retrieved
        /// </summary>
        public string DownloadUrl;
        /// <summary>
        /// Name of the file
        /// </summary>
        public string FileName;
        /// <summary>
        /// Last UTC time the file was modified
        /// </summary>
        public DateTime LastModified;
        /// <summary>
        /// Storage service's reported byte count
        /// </summary>
        public int Size;
    }

    [Serializable]
    public class GetFilesRequest : PlayFabRequestCommon
    {
        /// <summary>
        /// The entity to perform this action on.
        /// </summary>
        public EntityKey Entity;
    }

    [Serializable]
    public class GetFilesResponse : PlayFabResultCommon
    {
        /// <summary>
        /// The entity id and type.
        /// </summary>
        public EntityKey Entity;
        /// <summary>
        /// Collection of metadata for the entity's files
        /// </summary>
        public Dictionary<string,GetFileMetadata> Metadata;
        /// <summary>
        /// The current version of the profile, can be used for concurrency control during updates.
        /// </summary>
        public int ProfileVersion;
    }

    [Serializable]
    public class GetObjectsRequest : PlayFabRequestCommon
    {
        /// <summary>
        /// The entity to perform this action on.
        /// </summary>
        public EntityKey Entity;
        /// <summary>
        /// Determines whether the object will be returned as an escaped JSON string or as a un-escaped JSON object. Default is JSON
        /// object.
        /// </summary>
        public bool? EscapeObject;
    }

    [Serializable]
    public class GetObjectsResponse : PlayFabResultCommon
    {
        /// <summary>
        /// The entity id and type.
        /// </summary>
        public EntityKey Entity;
        /// <summary>
        /// Requested objects that the calling entity has access to
        /// </summary>
        public Dictionary<string,ObjectResult> Objects;
        /// <summary>
        /// The current version of the profile, can be used for concurrency control during updates.
        /// </summary>
        public int ProfileVersion;
    }

    [Serializable]
    public class InitiateFileUploadMetadata
    {
        /// <summary>
        /// Name of the file.
        /// </summary>
        public string FileName;
        /// <summary>
        /// Location the data should be sent to via an HTTP PUT operation.
        /// </summary>
        public string UploadUrl;
    }

    [Serializable]
    public class InitiateFileUploadsRequest : PlayFabRequestCommon
    {
        /// <summary>
        /// The entity to perform this action on.
        /// </summary>
        public EntityKey Entity;
        /// <summary>
        /// Names of the files to be set. Restricted to a-Z, 0-9, '(', ')', '_', '-' and '.'
        /// </summary>
        public List<string> FileNames;
        /// <summary>
        /// The expected version of the profile, if set and doesn't match the current version of the profile the operation will not
        /// be performed.
        /// </summary>
        public int? ProfileVersion;
    }

    [Serializable]
    public class InitiateFileUploadsResponse : PlayFabResultCommon
    {
        /// <summary>
        /// The entity id and type.
        /// </summary>
        public EntityKey Entity;
        /// <summary>
        /// The current version of the profile, can be used for concurrency control during updates.
        /// </summary>
        public int ProfileVersion;
        /// <summary>
        /// Collection of file names and upload urls
        /// </summary>
        public List<InitiateFileUploadMetadata> UploadDetails;
    }

    [Serializable]
    public class ObjectResult : PlayFabResultCommon
    {
        /// <summary>
        /// Un-escaped JSON object, if EscapeObject false or default.
        /// </summary>
        public object DataObject;
        /// <summary>
        /// Escaped string JSON body of the object, if EscapeObject is true.
        /// </summary>
        public string EscapedDataObject;
        /// <summary>
        /// Name of the object. Restricted to a-Z, 0-9, '(', ')', '_', '-' and '.'
        /// </summary>
        public string ObjectName;
    }

    public enum OperationTypes
    {
        Created,
        Updated,
        Deleted,
        None
    }

    [Serializable]
    public class SetObject
    {
        /// <summary>
        /// Body of the object to be saved. If empty and DeleteObject is true object will be deleted if it exists, or no operation
        /// will occur if it does not exist. Only one of Object or EscapedDataObject fields may be used.
        /// </summary>
        public object DataObject;
        /// <summary>
        /// Flag to indicate that this object should be deleted. Both DataObject and EscapedDataObject must not be set as well.
        /// </summary>
        public bool? DeleteObject;
        /// <summary>
        /// Body of the object to be saved as an escaped JSON string. If empty and DeleteObject is true object will be deleted if it
        /// exists, or no operation will occur if it does not exist. Only one of DataObject or EscapedDataObject fields may be used.
        /// </summary>
        public string EscapedDataObject;
        /// <summary>
        /// Name of object. Restricted to a-Z, 0-9, '(', ')', '_', '-' and '.'.
        /// </summary>
        public string ObjectName;
    }

    [Serializable]
    public class SetObjectInfo
    {
        /// <summary>
        /// Name of the object
        /// </summary>
        public string ObjectName;
        /// <summary>
        /// Optional reason to explain why the operation was the result that it was.
        /// </summary>
        public string OperationReason;
        /// <summary>
        /// Indicates which operation was completed, either Created, Updated, Deleted or None.
        /// </summary>
        public OperationTypes? SetResult;
    }

    [Serializable]
    public class SetObjectsRequest : PlayFabRequestCommon
    {
        /// <summary>
        /// The entity to perform this action on.
        /// </summary>
        public EntityKey Entity;
        /// <summary>
        /// Optional field used for concurrency control. By specifying the previously returned value of ProfileVersion from
        /// GetProfile API, you can ensure that the object set will only be performed if the profile has not been updated by any
        /// other clients since the version you last loaded.
        /// </summary>
        public int? ExpectedProfileVersion;
        /// <summary>
        /// Collection of objects to set on the profile.
        /// </summary>
        public List<SetObject> Objects;
    }

    [Serializable]
    public class SetObjectsResponse : PlayFabResultCommon
    {
        /// <summary>
        /// New version of the entity profile.
        /// </summary>
        public int ProfileVersion;
        /// <summary>
        /// New version of the entity profile.
        /// </summary>
        public List<SetObjectInfo> SetResults;
    }
}
#endif
