using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class RunPathSpawner : MonoBehaviour
{
	public bool debug;

	public LevelSegment[] levelSegmentsPrefabs;
	public int[] associatedActionIds;
	private int nextLevelSegmentIndex;

	private GameObject[] buttonIndicators;

	public Transform player;

	public int activeDistance;
	public int targetDistanceOffset;
	public int pastDistanceOffset;

	public float playerPosition;

	private List<LevelSegment> segments = new List<LevelSegment>();

	private List<LevelSegment> interactiveSegments = new List<LevelSegment>();
	private List<int> actionIds = new List<int>();

	void Start()
	{
		EditorClearAllSegments();
		List<LevelSegment> tmp = new List<LevelSegment> (levelSegmentsPrefabs);
		tmp.AddRange (levelSegmentsPrefabs);
		tmp.AddRange (levelSegmentsPrefabs);
		tmp.AddRange (levelSegmentsPrefabs);
		tmp.AddRange (levelSegmentsPrefabs);
		tmp.AddRange (levelSegmentsPrefabs);
		tmp.AddRange (levelSegmentsPrefabs);
		levelSegmentsPrefabs = tmp.ToArray ();
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

		if (nextLevelSegmentIndex < levelSegmentsPrefabs.Length) {
			while (nextLevelSegmentIndex < levelSegmentsPrefabs.Length && usedDistance < playerPosition + targetDistanceOffset) {
				LevelSegment prefab = levelSegmentsPrefabs[nextLevelSegmentIndex];
				int levelSegmentLenght = prefab.segmentLength;
				float xPosition = usedDistance + levelSegmentLenght / 2f;
				Vector3 position = new Vector3(xPosition, 0, 0);
				Quaternion rotation = Quaternion.identity;
				Transform parent = transform;
				LevelSegment instantiatedSegment = (LevelSegment)Instantiate(prefab, position, rotation, parent);

				segments.Add(instantiatedSegment);

				if (instantiatedSegment.interactive) {
					interactiveSegments.Add(instantiatedSegment);
					int actionId = associatedActionIds[nextLevelSegmentIndex];
					actionIds.Add(actionId);

					instantiatedSegment.SetupActionHint(actionId);
				}

				usedDistance += levelSegmentLenght;

				Debug.Log("Instantiated segment of index " + nextLevelSegmentIndex + ", posX " + position.x);

				nextLevelSegmentIndex++;
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
		for (int i = 0; i < interactiveSegments.Count; i++) {
			LevelSegment levelSegment = interactiveSegments[i];

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

	public void EditorInstantiateAllSegmentsTest()
	{
		EditorClearAllSegments();

		int distance = 0;

		foreach (LevelSegment prefab in levelSegmentsPrefabs) {
			float levelSegmentLenght = prefab.segmentLength;
			float xPosition = distance + levelSegmentLenght / 2;
			Vector3 position = new Vector3(xPosition, 0, 0);

			Quaternion rotation = Quaternion.identity;
			Transform parent = transform;
			LevelSegment instantiatedSegment = (LevelSegment)Instantiate(prefab, position, rotation, parent);

			segments.Add(instantiatedSegment);

			distance += (int)levelSegmentLenght;
		}
	}

	public void EditorClearAllSegments()
	{
		List<LevelSegment> segmentsToDestroy = new List<LevelSegment>();
		foreach (Transform child in transform) {
			LevelSegment segment = child.GetComponent<LevelSegment>();
			segmentsToDestroy.Add(segment);
		}

		foreach (LevelSegment segment in segmentsToDestroy) {
			if (Application.isPlaying) { 
				Destroy(segment.gameObject);
			} else {
				DestroyImmediate(segment.gameObject);
			}
		}

		segments.Clear();
	}

	private int CalcLength()
	{
		int totalLenght = 0;
		foreach (LevelSegment levelSegment in levelSegmentsPrefabs) {
			totalLenght += levelSegment.segmentLength;
		}
		return totalLenght;
	}
}
