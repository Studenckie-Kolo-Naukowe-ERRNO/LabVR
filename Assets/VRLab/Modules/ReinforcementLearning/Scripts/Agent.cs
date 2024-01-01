using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent
{
    public State[,] states; // Get the labyrinth size
    public int posX;
    public int posY;
    private float epsilon = 0.9f; // exploration probability
    private float a = 0.1f; // learning rate
    private float y = 0.9f; // discount factor
    public int steps = 0;
    public int generation = 0;

    public Agent(int xSize, int ySize, int startX, int startY) {
        posX = startX;
        posY = startY;

        states = new State[xSize, ySize];
        for (int i = 0; i < xSize; i++) {
            for (int j = 0; j < ySize; j++) {
                states[i, j] = new State();
            }
        }
        steps = 0;
        generation = 0;
}

    public int MakeDecision() {
        steps++;

        if (Random.Range(0f, 1f) < epsilon) {
            //best
            return states[posX, posY].GetBestAction();
        } else {
            //random
            return (int)Random.Range(0f, 4f);
        }
    }

    public void GiveReward(int reward, int action, int newX, int newY) {

        float prevQ = states[posX, posY].qualityValues[action];
        float maxQ = states[newX, newY].GetBestValue();

        if (reward == 100) {
            generation++;
            steps = 0;
        }

        //Q-Learning: prevQ = Q(t-1)+a(r+ y* max(Q) - prevQ)
        states[posX, posY].qualityValues[action] = prevQ + a * (reward + y * maxQ - prevQ);

        posX = newX;
        posY = newY;
    }
}

public class State {
    public float[] qualityValues = { 0, 0, 0, 0 };

    public int GetBestAction() {
        int bestAction = 0;
        float bestValue = -100;

        for (int i = 0; i < qualityValues.Length; i++) {
            if (qualityValues[i] > bestValue) {
                bestAction = i;
                bestValue = qualityValues[i];
            } else if (qualityValues[i] == bestValue) {
                if (Random.Range(0f, 1f) > 0.5f) {
                    bestAction = i;
                    bestValue = qualityValues[i];
                }
            }
        }

        return bestAction;
    }

    public float GetBestValue() {
        float bestValue = -100;

        foreach (float qValue in qualityValues) {
            if (qValue > bestValue) {
                bestValue = qValue;
            }
        }

        return bestValue;
    }
}
