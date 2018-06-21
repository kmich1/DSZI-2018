

import java.util.ArrayList;
import java.util.Collections;

public class Path{

    private ArrayList pathList = new ArrayList<Waypoint>();
    private double fitness = 0;
    private int distance = 0;

    public Path(){
        for (int i = 0; i < numberOfWaypoints(); i++) {
            pathList.add(null);
        }
    }

    public Path(ArrayList path){
        this.pathList = path;
    }

    public void generateIndividual() {
        for (int waypointIndex = 0; waypointIndex < numberOfWaypoints(); waypointIndex++) {
            setPath(waypointIndex, getWaypoint(waypointIndex));
        }
        Collections.shuffle(pathList.subList(1,pathList.size()));
    }

    public Waypoint getPoint(int pathPosition) {
        return (Waypoint) pathList.get(pathPosition);
    }

    public void setPath(int pathPosition, Waypoint wp) {
        pathList.set(pathPosition, wp);
        fitness = 0;
        distance = 0;
    }


    public double getFitness() {
        if (fitness == 0) {
            fitness = 1/(double)getDistance();
        }
        return fitness;
    }

    public int getDistance(){
        if (distance == 0) {
            int pathDistance = 0;
            for (int WaypointIndex=0; WaypointIndex < pathSize(); WaypointIndex++) {

                Waypoint fromWaypoint = getPoint(WaypointIndex);

                Waypoint destinationWaypoint;
                if(WaypointIndex+1 < pathSize()){
                    destinationWaypoint = getPoint(WaypointIndex+1);
                }
                else{
                        destinationWaypoint = getPoint(0);
                }
                pathDistance += fromWaypoint.distanceTo(destinationWaypoint);
            }
            distance = pathDistance;
        }
        return distance;
    }


    public int pathSize() {
        return pathList.size();
    }

    public boolean containsWaypoint(Waypoint waypoint){
        return pathList.contains(waypoint);
    }
    private static ArrayList destination = new ArrayList<Waypoint>();

    public static void addWaypoint(Waypoint wp) {
        destination.add(wp);
    }

    public static Waypoint getWaypoint(int index){
        return (Waypoint) destination.get(index);
    }

    public static int numberOfWaypoints(){
        return destination.size();
    }

    public static void reset() { destination.clear();}


    @Override
    public String toString() {
        String geneString = "";
        for (int i = 0; i < pathSize(); i++) {
            geneString += getPoint(i)+" ";
        }
        return geneString;
    }
}
