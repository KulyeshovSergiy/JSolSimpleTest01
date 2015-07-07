using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiffProcessor
{
	public class Processor
	{
	    private long iLimit;
		private SortedLimitedList<Double>.Entry oneEOItem;
		private SortedLimitedList<Double>.Entry oneMBETItem;
		private SortedLimitedList<Double>.Entry twoEOItem;
		private SortedLimitedList<Double>.Entry twoMBETItem;
		private SortedLimitedList<Double>.Entry remMBETItem;
		public Processor(long limit)
		{
			// TODO: initialize.
			iLimit=limit;
		}

		public void DoProcess(SortedLimitedList<Double> mustBeEqualTo, SortedLimitedList<Double> expectedOutput)
		{
			// TODO: make "mustBeEqualTo" list equal to "expectedOutput".
			// 0. Processor will be created once and then will be used billion times. 
			// 1. Use methods: AddFirst, AddLast, AddBefore, AddAfter, Remove to modify list.
			// 2. Do not change expectedOutput list.
			// 3. At any time number of elements in list could not exceed the "Limit". 
			// 4. "Limit" will be passed into Processor's constructor. All "mustBeEqualTo" and "expectedOutput" lists will have the same "Limit" value.
			// 5. At any time list elements must be in non-descending order.
			// 6. Implementation must perform minimal possible number of actions (AddFirst, AddLast, AddBefore, AddAfter, Remove).
			// 7. Implementation must be fast and do not allocate excess memory.
			
    	oneEOItem = expectedOutput.Last;
    	oneMBETItem = mustBeEqualTo.Last;
    	//Delete in mustBeEqualTo all which greater than last from expectedOutput
    	while(oneMBETItem!=null){
    		if(oneEOItem !=null && oneMBETItem.Value.CompareTo(oneEOItem.Value)>0){
				remMBETItem = oneMBETItem;
				oneMBETItem = oneMBETItem.Previous;
				mustBeEqualTo.Remove(remMBETItem);	    			
    		}else if(oneEOItem == null){
				remMBETItem = oneMBETItem;
				oneMBETItem = oneMBETItem.Previous;
				mustBeEqualTo.Remove(remMBETItem);	    			
    		} else {
    			oneMBETItem = oneMBETItem.Previous;
    		}
    	}			
    	//Delete in mustBeEqualTo all which not found in expectedOutput from the beginning
    	oneEOItem = expectedOutput.First;
    	int totalEOItems=0;
    	oneMBETItem = mustBeEqualTo.First;    	
    	while(oneEOItem != null && oneMBETItem!=null){
    		twoEOItem = oneEOItem.Next;
    		totalEOItems++;
    		int kolvoEOItems=1;
    		while(twoEOItem!=null && oneEOItem.Value.CompareTo(twoEOItem.Value)==0){
    			kolvoEOItems++;   
    			totalEOItems++;
    			twoEOItem=twoEOItem.Next;
    		}
    		while(oneMBETItem!=null && oneEOItem.Value.CompareTo(oneMBETItem.Value)>=0){
			if(oneEOItem.Value.CompareTo(oneMBETItem.Value)>0){
				remMBETItem = oneMBETItem;
				oneMBETItem = oneMBETItem.Next;
				mustBeEqualTo.Remove(remMBETItem);
			}else{
				twoMBETItem = oneMBETItem.Next;
				int kolvoMBETItems=1;
	    		while(twoMBETItem!=null && oneMBETItem.Value.CompareTo(twoMBETItem.Value)==0){
	    			kolvoMBETItems++;   
	    			twoMBETItem=twoMBETItem.Next;
	    		}
	    		while(kolvoMBETItems>kolvoEOItems){
					remMBETItem = oneMBETItem;
					oneMBETItem = oneMBETItem.Next;
					mustBeEqualTo.Remove(remMBETItem);	    			
	    			kolvoMBETItems--;
	    		}
				oneMBETItem = twoMBETItem;
			}
		}    		
    		oneEOItem = twoEOItem;
    	}
			
    	if(totalEOItems>iLimit){
    		throw new Exception("Number of elements in 'expectedOutput' exceed the 'Limit'.");
    	}
    	//Insert in mustBeEqualTo all which found in expectedOutput from the beginning

    	oneEOItem = expectedOutput.First;
    	oneMBETItem = mustBeEqualTo.First;    	
    	while(oneEOItem != null){
    		twoEOItem = oneEOItem.Next;
    		int kolvoEOItems=1;
    		while(twoEOItem!=null && oneEOItem.Value.CompareTo(twoEOItem.Value)==0){
    			kolvoEOItems++;   
    			twoEOItem=twoEOItem.Next;
     		}

    		if(oneMBETItem == null){
    			for(int i=0;i<kolvoEOItems;i++){
    				mustBeEqualTo.AddLast(oneEOItem.Value);
    			}
    			//oneMBETItem = mustBeEqualTo.Last;
    		}else if(oneEOItem.Value.CompareTo(oneMBETItem.Value)<0){

    			for(int i=0;i<kolvoEOItems;i++){
     				mustBeEqualTo.AddBefore(oneMBETItem,oneEOItem.Value);
    			}
   			
    		}else if(oneEOItem.Value.CompareTo(oneMBETItem.Value)==0){
				twoMBETItem = oneMBETItem.Next;
				kolvoEOItems--;
	    		while(twoMBETItem!=null && oneMBETItem.Value.CompareTo(twoMBETItem.Value)==0){
	    			kolvoEOItems--;   
	    			twoMBETItem=twoMBETItem.Next;
	    		}  
    			for(int i=0;i<kolvoEOItems;i++){
    				mustBeEqualTo.AddAfter(oneMBETItem,oneEOItem.Value);
    			}
	    		oneMBETItem = twoMBETItem;
    		}
    		
    		oneEOItem = twoEOItem;
    	}
			
			
			
			
		}
	}
}
