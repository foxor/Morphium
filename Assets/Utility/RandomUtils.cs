using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class RandomUtils {
	public static T Pick<T>(this IEnumerable<T> options) {
		List<T> list = options.ToList();
		return list[Random.Range(0, list.Count)];
	}
	public static T Pick<T>(this System.Array options) {
		return options.Cast<T>().Pick();
	}
}