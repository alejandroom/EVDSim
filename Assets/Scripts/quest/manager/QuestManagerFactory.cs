using System.Collections;

using edvsim.quest.objective;

namespace edvsim.quest.manager
{
	public class QuestManagerFactory {

		private QuestFactory questFactory;

		public QuestManagerFactory(GameObjectManager gom){
			questFactory = new QuestFactory (gom);
		}

		public QuestManager build(int numQM){
			switch (numQM) {
			case 1:
				return buildFirstQuestManager();
			case 2:
				return buildFirstQuestManager();
			default:
				return buildFirstQuestManager();
			}
		}

		private QuestManager buildFirstQuestManager(){
			return new QuestManagerBuilder ().quest (questFactory.build (1)).
				quest (questFactory.build (2)).quest (questFactory.build (3)).
					quest (questFactory.build (4)).quest (questFactory.build (5)).buildSequential ();
		}
	}
}