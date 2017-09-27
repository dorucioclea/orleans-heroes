import { Action } from "@ngrx/store";

import { HeroState, Hero } from "./hero.model";
import { HERO_ACTION_TYPE } from "./hero.action";
import { updateAllMapState } from "../../shared/utils/reducer.util";

const initialState: HeroState = {};

export function heroReducer(state: HeroState = initialState, action: Action): HeroState {
	switch (action.type) {
		case HERO_ACTION_TYPE.getSuccess: {
			const response: Hero[] = action.payload;
			return updateAllMapState<HeroState, Hero>(state, response);
		}
		default:
			return state;
	}
}