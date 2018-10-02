import {EntityFightSummary} from '@/models/EntityFightSummary';

export enum RankingSortMode {
    ByDamage,
}

export class FightEvaluator {
    public static giveValue(rankingMode: RankingSortMode, summary: EntityFightSummary): number {
        switch (rankingMode) {
            case RankingSortMode.ByDamage:
                return summary.damage;
        }
        return 0;
    }
}
