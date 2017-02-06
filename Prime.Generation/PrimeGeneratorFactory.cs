namespace Prime.Generation {
	public static class PrimeGeneratorFactory {
		public static IPrimeGenerator Create(PrimeGeneratorType generatorType) {
			IPrimeGenerator generator = new StaticPrimeGenerator();
			switch ( generatorType ) {
				case PrimeGeneratorType.SieveOfEratosthenes: {
					generator =  new SoEPrimeGenerator();
					break;
				}
				case PrimeGeneratorType.Sqrt: {
					generator =  new SqrtPrimeGenerator();
					break;
				}
				case PrimeGeneratorType.Static: {
					generator =  new StaticPrimeGenerator();
					break;
				}
			}
			return generator;
		}
	}


}
