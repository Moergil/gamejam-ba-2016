using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class RunPathSpawner : MonoBehaviour
{
	public bool debug;

	public SegmentDef initialSegment;
	public SegmentDef[] segmentDefs;

	private GameObject[] buttonIndicators;

	public Transform player;

	[HideInInspector]
	public int activeDistance;
	public int targetDistanceOffset;
	public int pastDistanceOffset;

	public int interactionDistance;

	[HideInInspector]
	public float playerPosition;

	private List<LevelSegment> segments = new List<LevelSegment>();

	private List<LevelSegment> interactiveSegments = new List<LevelSegment>();
	private List<int> actionIds = new List<int>();

	public int generatorSeed;

	void Start()
	{
		if (generatorSeed != 0) {
			UnityEngine.Random.InitState(generatorSeed);
		}
	}
	
	// Update is called once per frame
	void Update()
	{
		playerPosition = player.transform.position.x;

		PathBuilding();
	}

	public void PathBuilding()
	{
		List<LevelSegment> pastSegments = new List<LevelSegment>();

		int usedDistance = activeDistance;

		foreach (LevelSegment segment in segments) {
			int segmentLenght = segment.segmentLength;
			usedDistance += segmentLenght;

			if (usedDistance + pastDistanceOffset < playerPosition) {
				Debug.Log("Segment at posX " + usedDistance + " scheduled for remove.");
				pastSegments.Add(segment);
				activeDistance += segmentLenght;
				Destroy(segment.gameObject);
			}
		}

		if (debug) {
			Debug.Log("used " + usedDistance + ", active " + activeDistance);
		}

		while (usedDistance < playerPosition + targetDistanceOffset) {
			SegmentDef segmentDef;

			if (segments.Count == 0) {
				segmentDef = initialSegment;
			} else {
				int index = UnityEngine.Random.Range(0, segmentDefs.Length);
				segmentDef = segmentDefs[index];
			}

			foreach (SegmentActionStruct segmentActionStruct in segmentDef.segmentActionStructs) {
				LevelSegment prefab = segmentActionStruct.levelSegmentPrefab;
				int levelSegmentLenght = prefab.segmentLength;
				float xPosition = usedDistance + levelSegmentLenght / 2f;
				Vector3 position = new Vector3(xPosition, 0, 0);
				//float rotationY = (UnityEngine.Random.value >= 0.5f) ? 0 : 180;
				float rotationY = 0;
				Quaternion rotation = Quaternion.Euler(0, rotationY, 0);
				Transform parent = transform;
				LevelSegment instantiatedSegment = (LevelSegment)Instantiate(prefab, position, rotation, parent);

				segments.Add(instantiatedSegment);

				if (instantiatedSegment.interactive) {
					interactiveSegments.Add(instantiatedSegment);
					int actionId = segmentActionStruct.associatedAction;
					actionIds.Add(actionId);

					instantiatedSegment.SetupActionHint(actionId);
				}

				usedDistance += levelSegmentLenght;

				Debug.Log("Instantiated segment, posX " + position.x);
			}
		}

		foreach (LevelSegment segment in pastSegments) {
			segments.Remove(segment);

			int index = interactiveSegments.IndexOf(segment);
			if (index != -1) {
				interactiveSegments.RemoveAt(index);
				actionIds.RemoveAt(index);
			}
		}

	}

	public void OnAction(int actionId, bool positive)
	{
		int distance = 0;

		for (int i = 0; i < interactiveSegments.Count; i++) {
			LevelSegment levelSegment = interactiveSegments[i];

			float levelSegmentPosX = levelSegment.transform.position.x;
			float distanceToLevelSegment = levelSegmentPosX - levelSegment.segmentLength / 2f;

			if (playerPosition + interactionDistance < distanceToLevelSegment) {
				break;
			}

			if (!levelSegment.hintActivated) {
				levelSegment.ActivateHint();
			}

			distance += levelSegment.segmentLength;

			if (levelSegment.IsActionFinished()) {
				continue;
			}

			int levelSegmentActionId = actionIds[i];

			if (levelSegmentActionId == actionId) {
				Debug.Log("Action " + actionId + ", (" + positive + ") for segment " + levelSegment.gameObject.name);
				levelSegment.OnAction(positive);
				break;
			}
		}
	}

	[Serializable]
	public class SegmentDef
	{
		public SegmentActionStruct[] segmentActionStructs;

	}

	[Serializable]
	public class SegmentActionStruct
	{
		public LevelSegment levelSegmentPrefab;
		public int associatedAction;
	}
}
