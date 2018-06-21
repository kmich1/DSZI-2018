public class GeneticAlg {


    private static final double mutationRate = 0.01;
    private static final int tournamentSize = 5;

    public static Population evolvePopulation(Population pop) {
        Population newPopulation = new Population(pop.populationSize());

            newPopulation.savePath(0, pop.getFittest());

        for (int i = 0; i < newPopulation.populationSize(); i++) {

            Path parent1 = tournamentSelection(pop);
            Path parent2 = tournamentSelection(pop);

            Path child = crossover(parent1, parent2);

            newPopulation.savePath(i, child);
        }

        for (int i = 0; i < newPopulation.populationSize(); i++) {
            mutate(newPopulation.getPath(i));
        }

        return newPopulation;
    }


    public static Path crossover(Path parent1, Path parent2) {
        Path child = new Path();


        int startPos = (int) (Math.random() * parent1.pathSize());
        int endPos = (int) (Math.random() * parent1.pathSize());

        for (int i = 0; i < child.pathSize(); i++) {
            if (startPos < endPos && i > startPos && i < endPos) {
                child.setPath(i, parent1.getPoint(i));
            }
            else if (startPos > endPos) {
                if (!(i < startPos && i > endPos)) {
                    child.setPath(i, parent1.getPoint(i));
                }
            }
        }

        for (int i = 0; i < parent2.pathSize(); i++) {
            if (!child.containsWaypoint(parent2.getPoint(i))) {
                for (int ii = 0; ii < child.pathSize(); ii++) {
                    if (child.getPoint(ii) == null) {
                        child.setPath(ii, parent2.getPoint(i));
                        break;
                    }
                }
            }
        }
        return child;
    }

    private static void mutate(Path path) {
        for(int pointAtPos1=0; pointAtPos1 < path.pathSize(); pointAtPos1++){
            if(Math.random() < mutationRate){

                int pointAtPos2 = (int) (path.pathSize() * Math.random());
                Waypoint wp1 = path.getPoint(pointAtPos1);
                Waypoint wp2 = path.getPoint(pointAtPos2);
                path.setPath(pointAtPos2, wp1);
                path.setPath(pointAtPos1, wp2);
            }
        }
    }

    private static Path tournamentSelection(Population pop) {

        Population tournament = new Population(tournamentSize);

        for (int i = 0; i < tournamentSize; i++) {
            int randomId = (int) (Math.random() * pop.populationSize());
            tournament.savePath(i, pop.getPath(randomId));
        }
        Path fittest = tournament.getFittest();
        return fittest;
    }
}
