using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class RunPathManager : MonoBehaviour
{
	public bool debug;

	public LevelSegment[] levelSegmentsPrefabs;
	private int nextLevelSegmentIndex;

	public Transform player;

	public int activeDistance;
	public int targetDistanceOffset;
	public int pastDistanceOffset;

	public float playerPosition;
	private int runnedSegmentsDistance;

	private List<LevelSegment> segments = new List<LevelSegment>();

	void Start()
	{
		EditorClearAllSegments();
	}
	
	// Update is called once per frame
	void Update()
	{
		playerPosition = player.transform.position.x;

		if (nextLevelSegmentIndex < levelSegmentsPrefabs.Length) {
			PathBuilding();
		}
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
			LevelSegment prefab = levelSegmentsPrefabs[nextLevelSegmentIndex];
			int levelSegmentLenght = prefab.segmentLength;
			float xPosition = usedDistance + levelSegmentLenght / 2f;
			Vector3 position = new Vector3(xPosition, 0, 0);
			Quaternion rotation = Quaternion.identity;
			Transform parent = transform;
			LevelSegment instantiatedSegment = (LevelSegment)Instantiate(prefab, position, rotation, parent);

			segments.Add(instantiatedSegment);

			usedDistance += levelSegmentLenght;

			Debug.Log("Instantiated segment of index " + nextLevelSegmentIndex + ", posX " + position.x);

			nextLevelSegmentIndex++;
		}


		foreach (LevelSegment segment in pastSegments) {
			segments.Remove(segment);
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
		foreach (Transform child in transform) {
			LevelSegment segment = child.GetComponent<LevelSegment>();
			Destroy(segment.gameObject);
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
