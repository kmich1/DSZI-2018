
public class Waypoint {
    int x;
    int y;

    public Waypoint(int x, int y){
        this.x = x;
        this.y = y;
    }

    public int getX(){
        return this.x;
    }

    public int getY(){
        return this.y;
    }

    public double distanceTo(Waypoint wp){
        int xDistance = Math.abs(getX() - wp.getX());
        int yDistance = Math.abs(getY() - wp.getY());
        double distance = Math.sqrt( (xDistance*xDistance) + (yDistance*yDistance) );

        return distance;
    }

    @Override
    public String toString(){
        return getX()+" "+getY();
    }
}
