using System;
using System.Collections;
using System.Collections.Generic;

public static class CoordinateHelpers
{
    static int CubeDistance(Hex origin, Hex end) {
        return (Math.Abs(origin.xPos - end.xPos) + Math.Abs(origin.yPos - end.yPos) + Math.Abs(origin.zPos - end.zPos)) / 2;
    }

    static float Lerp(int origin, int end, float interval) {
		return origin + (end - origin) * interval;
	}

    static int[] CubeRound(float[] cube) {
        int rx = (int)Math.Round(cube[0],0);
        int ry = (int)Math.Round(cube[1],0);
        int rz = (int)Math.Round(cube[2],0);

        float x_diff = Math.Abs(rx - cube[0]);
        float y_diff = Math.Abs(ry - cube[1]);
        float z_diff = Math.Abs(rz - cube[2]);

        if (x_diff > y_diff && x_diff > z_diff) {
            rx = -ry-rz;
        }
        else if (y_diff > z_diff) {
            ry = -rx-rz;
        }
        else
            rz = -rx-ry;

        return new int[] {rx, ry, rz};
    }

	static float[] CubeLerp(Hex a,Hex b, float t) {
		return new float[] { Lerp(a.xPos, b.xPos, t), Lerp(a.yPos, b.yPos, t), Lerp(a.zPos, b.zPos, t)};
	}

	public static List<int[]> CalculateLine(Hex origin, Hex end) {
		int N = CubeDistance(origin, end);
		List<int[]> results = new List<int[]>();
		for (int i = 0; i < N; i++) {
		    results.Add(CubeRound(CubeLerp(origin, end, 1.0f/N * i)));
        }
		return results;
	}
}