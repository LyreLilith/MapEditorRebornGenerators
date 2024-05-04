



using MapEditorReborn.API.Features.Serializable;

namespace MapEditorReborn.API.Features.Objects;
using MapGeneration.Distributors;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

public class GeneratorObject : MapEditorObject
{


    private void Awake()
    {
        StructurePositionSync = GetComponent<StructurePositionSync>();
    }

    public override MapEditorObject Init(SchematicBlockData block)
    {
        base.Init(block);

        Base = new SerializableGenerator()
        {
            Position = block.Position,
            Rotation = block.Rotation,
            Scale = block.Scale
        };
            
        return this;
    }


   public SerializableGenerator Base;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public GeneratorObject Init(SerializableGenerator serializableObjectData)
    {
            
        transform.position = serializableObjectData.Position;
        transform.rotation =  Quaternion.Euler(serializableObjectData.Rotation);
        transform.localScale = serializableObjectData.Scale;
        Base = serializableObjectData;
        UpdateObject();
        return this;
    }

  
        
    /// <summary>
    /// Gets the <see cref="StructurePositionSync"/> of the object.
    /// </summary>
    public StructurePositionSync StructurePositionSync { get; private set; }

    /// <inheritdoc cref="UpdateObject()"/>
    public override void UpdateObject()
    {
        StructurePositionSync.Network_position = transform.position;
        StructurePositionSync.Network_rotationY = (sbyte)Mathf.RoundToInt(transform.rotation.eulerAngles.y / 5.625f);
            
        base.UpdateObject();
    }
}
