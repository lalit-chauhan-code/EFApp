﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFServices.DTOs;
using EFData.Models;
using EFData.Repositories;
using EFServices.Interfaces;

namespace EFServices.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<IEnumerable<CourseDto>> GetAllAsync()
        {
            try
            {
                var courses = await _courseRepository.GetAllAsync();
                return courses.Select(c => new CourseDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    //Credits = c.Credits
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAllAsync: {ex.Message}");
                throw;
            }

        }

        public async Task<CourseDto?> GetByIdAsync(int id)
        {
            try { 
                var course = await _courseRepository.GetByIdAsync(id);
                if (course == null) return null;

                return new CourseDto
                {
                    Id = course.Id,
                    Title = course.Title,
                    //Credits = course.Credits
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetByIdAsync: {ex.Message}");
                throw;
            }
}

        public async Task<CourseDto> CreateAsync(CreateCourseDto dto)
        {
            try 
            { 
                var course = new Course
                {
                    Title = dto.Title,
                    //Credits = dto.Credits
                };

                await _courseRepository.AddAsync(course);
                await _courseRepository.SaveChangesAsync();

                return new CourseDto
                {
                    Id = course.Id,
                    Title = course.Title,
                    //Credits = course.Credits
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(int id, CreateCourseDto dto)
        {
            try 
            { 
                var course = await _courseRepository.GetByIdAsync(id);
                if (course == null) return false;

                course.Title = dto.Title;
                //course.Credits = dto.Credits;
                course.UpdatedAt = DateTime.UtcNow;
                _courseRepository.Update(course);
                await _courseRepository.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try 
            { 
                var course = await _courseRepository.GetByIdAsync(id);
                if (course == null) return false;
                course.DeletedAt = DateTime.UtcNow;
                _courseRepository.Delete(course);
                await _courseRepository.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteAsync: {ex.Message}");
                throw;
            }
        }
    }
}