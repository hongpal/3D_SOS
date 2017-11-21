using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

    public GameObject BaseObject;

    // Use this for initialization
    void Start()
    {
        Vector3 dim1 = new Vector3(1f, 0.5f, 3f);
        Vector3 dim2 = new Vector3(3f, 0.5f, 1f);
        for (int i = 0; i < 10; ++i)
        {
            AddBox(new Vector3(-1f, 0.5f * i, 0f), dim1);
            AddBox(new Vector3(0f, 0.5f * i, 0f), dim1);
            AddBox(new Vector3(1f, 0.5f * i, 0f), dim1);

            ++i;
            AddBox(new Vector3(0f, 0.5f * i, -1f), dim2);
            AddBox(new Vector3(0f, 0.5f * i, 0f), dim2);
            AddBox(new Vector3(0f, 0.5f * i, 1f), dim2);
        }
    }

    void AddBox(Vector3 position, Vector3 scale)
    {
        GameObject newObject = GameObject.Instantiate(BaseObject) as GameObject;
        newObject.transform.position = position;
        newObject.transform.localScale = scale;
        
        newObject.GetComponent<Rigidbody>().mass = 40;

        newObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        newObject.GetComponent<Rigidbody>().transform.localScale += new Vector3(0.025f, 0.025f, 0.025f);

        /*
		NgObject *addBox(String matName, MatEnum m, Vector3 &pos, Vector3 dim = Vector3(1,1,1), Vector3 velocity = Vector3::ZERO, Matrix3 rot=Matrix3::IDENTITY) {
		{
			e->setMaterialName(matName);
			e->setNormaliseNormals(true);

			NxBoxShapeDesc boxDesc;
			NxBodyDesc bodyDesc;
			NxActorDesc actorDesc;

			// make dimensions 0.025 larger than 3d geom to compensate for separation penalty
			boxDesc.dimensions = NxVec3(dim.x+0.025, dim.y+0.025, dim.z+0.025);
			boxDesc.materialIndex = m;
			bodyDesc.linearVelocity = toNx(velocity);
			actorDesc.shapes.pushBack(&boxDesc);
			actorDesc.body = &bodyDesc;
			actorDesc.globalPose.t = toNx(pos);
			actorDesc.globalPose.M = toNx(rot);
			actorDesc.density = getDensity(m);

			NgObject *obj = mNogredex->addObject(actorDesc);
			obj->node->attachObject(e);
			obj->node->setScale(dim);
		}
		*/

    }

    // Update is called once per frame
    void Update()
    {

    }
}
