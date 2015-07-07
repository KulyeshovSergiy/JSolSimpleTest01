package diffprocessor;

/**
 * Created by VavilauA on 6/19/2015.
 */
public class Processor {
	private long iLimit;
    public Processor(long limit) {
        // TODO: initialize.
    	iLimit = limit;
    }

    public void doProcess(SortedLimitedList<Double> mustBeEqualTo, SortedLimitedList<Double> expectedOutput) {
        // TODO: make "mustBeEqualTo" list equal to "expectedOutput".
        // 0. Processor will be created once and then will be used billion times.
        // 1. Use methods: AddFirst, AddLast, AddBefore, AddAfter, Remove to modify list.
        // 2. Do not change expectedOutput list.
        // 3. At any time number of elements in list could not exceed the "Limit".
        // 4. "Limit" will be passed into Processor's constructor. All "mustBeEqualTo" and "expectedOutput" lists will have the same "Limit" value.
        // 5. At any time list elements must be in non-descending order.
        // 6. Implementation must perform minimal possible number of actions (AddFirst, AddLast, AddBefore, AddAfter, Remove).
        // 7. Implementation must be fast and do not allocate excess memory.
    	
    	
    	Entry oneEOItem = expectedOutput.getLast();
    	Entry oneMBETItem = mustBeEqualTo.getLast();
    	//Delete in mustBeEqualTo all which greater than last from expectedOutput
    	while(oneMBETItem!=null){
    		if(oneEOItem !=null && oneMBETItem.getValue().compareTo(oneEOItem.getValue())>0){
				Entry remMBETItem = oneMBETItem;
				oneMBETItem = oneMBETItem.getPrevious();
				mustBeEqualTo.remove(remMBETItem);	    			
    		}else if(oneEOItem == null){
				Entry remMBETItem = oneMBETItem;
				oneMBETItem = oneMBETItem.getPrevious();
				mustBeEqualTo.remove(remMBETItem);	    			
    		} else {
    			oneMBETItem = oneMBETItem.getPrevious();
    		}
    	}
    	//Delete in mustBeEqualTo all which not found in expectedOutput from the beginning
    	oneEOItem = expectedOutput.getFirst();
    	int totalEOItems=0;
    	oneMBETItem = mustBeEqualTo.getFirst();    	
    	while(oneEOItem != null && oneMBETItem!=null){
    		Entry twoEOItem = oneEOItem.getNext();
    		totalEOItems++;
    		int kolvoEOItems=1;
    		while(twoEOItem!=null && oneEOItem.getValue().compareTo(twoEOItem.getValue())==0){
    			kolvoEOItems++;   
    			totalEOItems++;
    			twoEOItem=twoEOItem.getNext();
    			
    		}
    		while(oneMBETItem!=null && oneEOItem.getValue().compareTo(oneMBETItem.getValue())>=0){
			if(oneEOItem.getValue().compareTo(oneMBETItem.getValue())>0){
				Entry remMBETItem = oneMBETItem;
				oneMBETItem = oneMBETItem.getNext();
				mustBeEqualTo.remove(remMBETItem);
			}else{
				Entry twoMBETItem = oneMBETItem.getNext();
				int kolvoMBETItems=1;
	    		while(twoMBETItem!=null && oneMBETItem.getValue().compareTo(twoMBETItem.getValue())==0){
	    			kolvoMBETItems++;   
	    			twoMBETItem=twoMBETItem.getNext();
	    		}
	    		while(kolvoMBETItems>kolvoEOItems){
					Entry remMBETItem = oneMBETItem;
					oneMBETItem = oneMBETItem.getNext();
					mustBeEqualTo.remove(remMBETItem);	    			
	    			kolvoMBETItems--;
	    		}
				oneMBETItem = twoMBETItem;
			}
		}    		
    		oneEOItem = twoEOItem;
    	}
    	
    	
    	
    	if(totalEOItems>iLimit){
    		throw new InvalidStateException("Number of elements in 'expectedOutput' exceed the 'Limit'.");
    	}
    	
    	//Insert in mustBeEqualTo all which found in expectedOutput from the beginning

    	oneEOItem = expectedOutput.getFirst();
    	oneMBETItem = mustBeEqualTo.getFirst();    	
    	while(oneEOItem != null){
    		Entry twoEOItem = oneEOItem.getNext();
    		int kolvoEOItems=1;
    		while(twoEOItem!=null && oneEOItem.getValue().compareTo(twoEOItem.getValue())==0){
    			kolvoEOItems++;   
    			twoEOItem=twoEOItem.getNext();
     		}

    		if(oneMBETItem == null){
    			for(int i=0;i<kolvoEOItems;i++){
    				mustBeEqualTo.addLast((Double) oneEOItem.getValue());
    			}
    			//oneMBETItem = mustBeEqualTo.getLast();
    		}else if(oneEOItem.getValue().compareTo(oneMBETItem.getValue())<0){

    			for(int i=0;i<kolvoEOItems;i++){
     				mustBeEqualTo.addBefore(oneMBETItem,(Double) oneEOItem.getValue());
    			}
   			
    		}else if(oneEOItem.getValue().compareTo(oneMBETItem.getValue())==0){
				Entry twoMBETItem = oneMBETItem.getNext();
				kolvoEOItems--;
	    		while(twoMBETItem!=null && oneMBETItem.getValue().compareTo(twoMBETItem.getValue())==0){
	    			kolvoEOItems--;   
	    			twoMBETItem=twoMBETItem.getNext();
	    		}  
    			for(int i=0;i<kolvoEOItems;i++){
    				mustBeEqualTo.addAfter(oneMBETItem,(Double) oneEOItem.getValue());
    			}
	    		oneMBETItem = twoMBETItem;
    		}
    		
    		oneEOItem = twoEOItem;
    	}


    	
    }
}

