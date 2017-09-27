
import { Injectable } from "@angular/core";
import { Action } from "@ngrx/store";
import { Hero } from "./hero.model";

const ACTION_PREFIX: string = "[Hero]";

export const HERO_ACTION_TYPE: any = {
	get: `${ACTION_PREFIX} Get`,
	getSuccess: `${ACTION_PREFIX} Get Success`,
	getFail: `${ACTION_PREFIX} Get Fail`,
};

@Injectable()
export class HeroAction {

	get(key: string): Action {
		return {
			type: HERO_ACTION_TYPE.get,
			payload: key
		};
	}

	getSuccess(hero: Hero): Action {
		return {
			type: HERO_ACTION_TYPE.getSuccess,
			payload: hero
		};
	}

	getFail(error: any): Action {
		return {
			type: HERO_ACTION_TYPE.getFail,
			payload: error
		};
	}

}