import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.io.PrintWriter;

public class TSP_GA {

    public static void main(String[] args) throws IOException {

        BufferedReader in = new BufferedReader(new FileReader("inputga.txt"));
        String line;
        line = in.readLine();
        in.close();
        String[] parts = line.split(" ");
        int k=0;
        while(k<=parts.length) {
            if (k == parts.length)
                break;
            else {
                Path.addWaypoint(new Waypoint(Integer.parseInt(parts[k]), Integer.parseInt(parts[k + 1])));
                k = k + 2;
            }
        }


            /*Waypoint wp = new Waypoint(4, 4);
            Path.addWaypoint(wp);
            Waypoint wp2 = new Waypoint(5 ,7);
            Path.addWaypoint(wp2);
            Waypoint wp3 = new Waypoint(3, 8);
            Path.addWaypoint(wp3);
            Waypoint wp4 = new Waypoint(3, 10);
            Path.addWaypoint(wp4);
            Waypoint wp5 = new Waypoint(1, 2);
            Path.addWaypoint(wp5);
            Waypoint wp6 = new Waypoint(8, 8);
            Path.addWaypoint(wp6);
            Waypoint wp7 = new Waypoint(5, 6);
            Path.addWaypoint(wp7);
            Waypoint wp8 = new Waypoint(4, 3);
            Path.addWaypoint(wp8);
            Waypoint wp9 = new Waypoint(10, 8);
            Path.addWaypoint(wp9);
            Waypoint wp10 = new Waypoint(3, 3);
            Path.addWaypoint(wp10);
            Waypoint wp11 = new Waypoint(7, 2);
            Path.addWaypoint(wp11);
            Waypoint wp12 = new Waypoint(3, 9);
            Path.addWaypoint(wp12);
            Waypoint wp13 = new Waypoint(6, 6);
            Path.addWaypoint(wp13);
            Waypoint wp14 = new Waypoint(5, 1);
            Path.addWaypoint(wp14);
            Waypoint wp15 = new Waypoint(4, 9);
            Path.addWaypoint(wp15);

            Waypoint p = wp;
            */
        Population pop = new Population(50);



        for (int i = 0; i < 120; i++) {
            pop = GeneticAlg.evolvePopulation(pop);
        }





        Waypoint end;
        end = pop.getFittest().getPoint(0);


        if(end.getX()==Path.getWaypoint(0).getX()&&end.getY()==Path.getWaypoint(0).getY())
        {
            System.out.println(pop.getFittest());
            PrintWriter writer = new PrintWriter("gaoutput.txt", "UTF-8");
            writer.println(pop.getFittest());
            writer.close();

        }
        else
        {
            Path.reset();
            main(args);
        }


    }
}
