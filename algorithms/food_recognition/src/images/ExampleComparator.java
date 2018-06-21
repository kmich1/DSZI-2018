/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package images;

import java.util.Comparator;

/**
 *
 * @author HP
 */
public class ExampleComparator implements Comparator<Example> {
    @Override
	public int compare(Example o1, Example o2)
	{
		return (o1.get_label()).compareTo(o2.get_label());
}
}
