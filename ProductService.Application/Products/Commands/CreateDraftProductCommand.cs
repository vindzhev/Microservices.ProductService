namespace ProductService.Application.Products.Commands
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    
    using MediatR;
    using AutoMapper;
    
    using ProductService.Domain.Entities;
    
    using MicroservicesPOC.Shared.Common.Models;
    
    using ProductService.Application.Common.Interfaces;
    using ProductService.Application.Common.Models.Cover;
    using ProductService.Application.Common.Models.Product;
    using ProductService.Application.Common.Models.Question;

    public class CreateDraftProductCommand : IRequest<Guid>
    {
        public CreateDraftProductCommand(ProductForCreationDTO product) => 
            this.Model = product ?? throw new ArgumentNullException(nameof(product));

        public ProductForCreationDTO Model { get; set; }

        public class CreateDraftProductCommandHandler : IRequestHandler<CreateDraftProductCommand, Guid>
        {
            private readonly IMapper _mapper;
            private readonly IProductRepository _productRepository;

            public CreateDraftProductCommandHandler(IProductRepository productRepository, IMapper mapper)
            {
                this._mapper = mapper ?? 
                    throw new ArgumentNullException(nameof(mapper));

                this._productRepository = productRepository ?? 
                    throw new ArgumentNullException(nameof(productRepository));
            }

            public async Task<Guid> Handle(CreateDraftProductCommand request, CancellationToken cancellationToken)
            {
                Product product = Product.CreateDraft(
                    request.Model.Code,
                    request.Model.Name,
                    request.Model.Image,
                    request.Model.Description,
                    request.Model.InsuredLimit);

                foreach (CoverForCreationDTO cover in request.Model.Covers)
                    product.AddCover(cover.Code, cover.Name, cover.Description, cover.Optional, cover.TotalInsured);

                ICollection<Question> questions = new List<Question>();
                foreach (QuestionorCreationDTO question in request.Model.Questions)
                {
                    switch (question.Type)
                    {
                        case QuestionType.Numeric:
                            questions.Add(new NumericQuestion(question.Code, question.Index, question.Text));
                            break;
                        case QuestionType.Text:
                            questions.Add(new DateQuestion(question.Code, question.Index, question.Text));
                            break;
                        case QuestionType.Choice:
                            questions.Add(new ChoiceQuestion(question.Code, question.Index, question.Text, 
                                (question.Choices != null && question.Choices.Any()) ? _mapper.Map<ICollection<Choice>>(question.Choices) : ChoiceQuestion.YesNoChoice()));
                            break;
                    }
                }

                product.AddQuestions(questions);

                await this._productRepository.Add(product, cancellationToken);
                await this._productRepository.SaveChangesAsync();

                return product.Id;
            }
        }
    }
}
